﻿namespace GitHubFlowVersion
{
    public class NextSemverCalcualtor : INextSemverCalcualtor
    {
        private readonly INextVersionTxtFileFinder _nextVersionTxtFileFinder;
        private readonly ILastTaggedReleaseFinder _lastTaggedReleaseFinder;

        public NextSemverCalcualtor(
            INextVersionTxtFileFinder nextVersionTxtFileFinder,
            ILastTaggedReleaseFinder lastTaggedReleaseFinder)
        {
            _nextVersionTxtFileFinder = nextVersionTxtFileFinder;
            _lastTaggedReleaseFinder = lastTaggedReleaseFinder;
        }

        public SemanticVersion NextVersion()
        {
            SemanticVersion fileVersion = _nextVersionTxtFileFinder.GetNextVersion();
            SemanticVersion lastRelease = _lastTaggedReleaseFinder.GetVersion().SemVer;
            if (fileVersion <= lastRelease)
            {
                return new SemanticVersion(lastRelease.Major, lastRelease.Minor, lastRelease.Patch + 1);
            }

            return fileVersion;
        }
    }
}