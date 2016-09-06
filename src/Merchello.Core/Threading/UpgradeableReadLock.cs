﻿namespace Merchello.Core.Threading
{
    using System;
    using System.Threading;

    /// <summary>
    /// Provides a convenience methodology for implementing locked access to resources. 
    /// </summary>
    /// <remarks>
    /// Intended as an infrastructure class.
    /// UMBRACO_SRC Direct port of Umbraco internal interface to get rid of hard dependency
    /// </remarks>
    public class UpgradeableReadLock : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;
		private bool _upgraded = false;

        /// <summary>
		/// Initializes a new instance of the <see cref="ReadLock"/> class.
        /// </summary>
        /// <param name="rwLock">The rw lock.</param>
		public UpgradeableReadLock(ReaderWriterLockSlim rwLock)
        {
            this._rwLock = rwLock;
            this._rwLock.EnterUpgradeableReadLock();
        }

		public void UpgradeToWriteLock()
		{
			this._rwLock.EnterWriteLock();
			this._upgraded = true;
		}

        void IDisposable.Dispose()
        {
			if (this._upgraded)
			{
				this._rwLock.ExitWriteLock();
			}
			this._rwLock.ExitUpgradeableReadLock();
        }
    }
}