// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrawlSettings.cs" company="pzcast">
//   (C) 2015 pzcast. All rights reserved.
// </copyright>
// <summary>
//   The crawl settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SDownloader
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The crawl settings.
    /// </summary>
    [Serializable]
    public class SpiderSettings
    {
        #region Fields

        /// <summary>
        /// The timeout.
        /// </summary>
        private int timeout = 15000;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpiderSettings"/> class.
        /// </summary>
        public SpiderSettings() {
            this.AutoSpeedLimit = false;
            this.EscapeLinks = new List<string>();
            this.KeepCookie = true;
            this.HrefKeywords = new List<string>();
            this.TextKeywords = new List<string>();
            this.RegularFilterExpressions = new List<string>();
            this.SeedsAddress = new List<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether auto speed limit.
        /// </summary>
        public bool AutoSpeedLimit { get; set; }

        /// <summary>
        /// Gets the escape links.
        /// </summary>
        public List<string> EscapeLinks { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether keep cookie.
        /// </summary>
        public bool KeepCookie { get; set; }

        /// <summary>
        /// Gets the href keywords.
        /// </summary>
        public List<string> HrefKeywords { get; private set; }

        /// <summary>
        /// Gets the href keywords.
        /// </summary>
        public List<string> TextKeywords { get; private set; }

        /// <summary>
        /// Gets the regular filter expressions.
        /// </summary>
        public List<string> RegularFilterExpressions { get; private set; }

        /// <summary>
        /// Gets  the seeds address.
        /// </summary>
        public List<string> SeedsAddress { get; private set; }

        /// <summary>
        /// Gets or sets the timeout.
        /// </summary>
        public int Timeout {
            get {
                return this.timeout;
            }

            set {
                this.timeout = value;
            }
        }

        public string imgType { get; set; }
        public string domain { get; set; }
        public string siteName { get; set; }
        public string savePath { get; set; }
        public long startPage { get; set; }
        public long endPage { get; set; }

        #endregion
    }
}