using System.ComponentModel.Design;
using LibGit2Sharp;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using MsConfigService.Models;

namespace MsConfigService.Services
{
    public class GitConfigService : IGitConfigService
    {
        private readonly GitConfigServerSettings _gitSettings;
        private readonly string _localRepoPath;

        public GitConfigService(IOptions<GitConfigServerSettings> gitSettings)
        {
            _gitSettings = gitSettings.Value;
            _localRepoPath = Path.Combine(Path.GetTempPath(), "config-repo"); // Ruta temporal para clonar el repositorio
            EnsureRepoCloned();
        }

        private void EnsureRepoCloned()
        {
            if (!Directory.Exists(_localRepoPath))
            {
                Directory.CreateDirectory(_localRepoPath);
                Repository.Clone(_gitSettings.Uri, _localRepoPath);
            }
            else
            {
                // Pull latest changes (simplificado, considera manejo de errores y autenticación)
                using (var repo = new Repository(_localRepoPath))
                {
                    var remote = repo.Network.Remotes["origin"] ?? repo.Network.Remotes.FirstOrDefault();
                    if (remote != null)
                    {
                        var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                        Commands.Pull(repo, new Signature("ConfigServer", "configserver@example.com", new System.DateTimeOffset()), new PullOptions { });
                    }
                }
            }
        }

        public string GetConfiguration(string applicationName, string profile)
        {
            string searchPath = _gitSettings.SearchPaths?.FirstOrDefault() ?? "";
            string label = profile ?? _gitSettings.DefaultLabel ?? "main";
            string filePath = Path.Combine(_localRepoPath, searchPath, applicationName, $"{profile}.json");

            try
            {
                // Checkout la rama o etiqueta correcta (simplificado)
                using (var repo = new Repository(_localRepoPath))
                {
                    var branch = repo.Branches.FirstOrDefault(b => b.FriendlyName == label);
                    if (branch != null)
                    {
                        Commands.Checkout(repo, branch);
                    }
                    else
                    {
                        // Intenta con una etiqueta si no se encuentra la rama
                        var tag = repo.Tags.FirstOrDefault(t => t.FriendlyName == label);
                        if (tag != null)
                        {
                            Commands.Checkout(repo, tag.Target.Sha);
                        }
                    }
                }

                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                else
                {
                    return null; // O lanzar una excepción indicando que no se encontró
                }
            }
            catch (RepositoryNotFoundException)
            {
                // Manejar el caso donde el repositorio no se clonó correctamente
                return null;
            }
            catch (CheckoutException)
            {
                // Manejar el caso donde no se pudo hacer checkout de la rama/etiqueta
                return null;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}