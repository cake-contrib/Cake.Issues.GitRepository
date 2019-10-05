namespace Cake.Issues.GitRepository
{
    /// <summary>
    /// Settings for <see cref="GitRepositoryIssuesAliases"/>.
    /// </summary>
    public class GitRepositoryIssuesSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the repository should be checked for
        /// binary files not tracked by Git LFS.
        /// Requires Git and Git-LFS to be available through Cake tool resolution.
        /// </summary>
        public bool CheckBinaryFilesTrackedByLfs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the repository should be checked for
        /// binary files path length.
        /// </summary>
        public bool CheckBinaryFilesPathLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the length value to be checked in the Path length check.
        /// </summary>
        public int BinaryFilesPathLength { get; set; }
    }
}
