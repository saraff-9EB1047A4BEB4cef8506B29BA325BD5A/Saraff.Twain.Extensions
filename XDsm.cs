/* Этот файл является частью библиотеки Saraff.Twain.Extensions
 * © SARAFF SOFTWARE (Кирножицкий Андрей), 2017.
 * Saraff.TwainX.Extensions - свободная программа: вы можете перераспространять ее и/или
 * изменять ее на условиях Меньшей Стандартной общественной лицензии GNU в том виде,
 * в каком она была опубликована Фондом свободного программного обеспечения;
 * либо версии 3 лицензии, либо (по вашему выбору) любой более поздней
 * версии.
 * Saraff.TwainX.Extensions распространяется в надежде, что она будет полезной,
 * но БЕЗО ВСЯКИХ ГАРАНТИЙ; даже без неявной гарантии ТОВАРНОГО ВИДА
 * или ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. Подробнее см. в Меньшей Стандартной
 * общественной лицензии GNU.
 * Вы должны были получить копию Меньшей Стандартной общественной лицензии GNU
 * вместе с этой программой. Если это не так, см.
 * <http://www.gnu.org/licenses/>.)
 * 
 * This file is part of Saraff.Twain.Extensions.
 * © SARAFF SOFTWARE (Kirnazhytski Andrei), 2017.
 * Saraff.TwainX.Extensions is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * Saraff.TwainX.Extensions is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public License
 * along with Saraff.TwainX.Extensions. If not, see <http://www.gnu.org/licenses/>.
 * 
 * PLEASE SEND EMAIL TO:  twain@saraff.ru.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Saraff.TwainX.Extensions {

    /// <summary>
    /// Represent a Data Source Manager of a TWAIN.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    public sealed class XDsm:Component {
        private bool _isOwns=false;
        private TwainX _twainX = null;

        private XDsm() {
        }

        /// <summary>
        /// Create instance of a XDsm class.
        /// </summary>
        /// <param name="isShowUI">if set to <c>true</c> is show UI of a Data Source.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        public static XDsm Create(bool isShowUI = false,TwLanguage language = TwLanguage.RUSSIAN,TwCountry country = TwCountry.BELARUS) {
            return XDsm.Create(
                new TwainX {
                    ShowUI=isShowUI,
                    Language=language,
                    Country=country
                },
                true);
        }

        /// <summary>
        /// Create instance of a XDsm class.
        /// </summary>
        /// <param name="twainX">Instance of the TwainX class.</param>
        /// <param name="isOwns">If set to <c>true</c> is owner of instance of the TwainX class.</param>
        /// <returns></returns>
        public static XDsm Create(TwainX twainx,bool isOwns = false) {
            XDsm _dsm = null;
            try {
                return _dsm=new XDsm {
                    _twainX=twainx,
                    _isOwns=isOwns
                };
            } finally {
                twainx.EndXfer+=_dsm._EndXfer;
                twainx.XferDone+=_dsm._XferDone;
                twainx.SetupMemXferEvent+=_dsm._SetupMemXferEvent;
                twainx.MemXferEvent+=_dsm._MemXferEvent;
                twainx.SetupFileXferEvent+=_dsm._SetupFileXferEvent;
                twainx.FileXferEvent+=_dsm._FileXferEvent;
                twainx.AcquireCompleted+=_dsm._AcquireCompleted;
                twainx.AcquireError+=_dsm._AcquireError;
            }
        }

        #region Events Handlers

        private void _EndXfer(object sender,TwainX.EndXferEventArgs e) {
            this.NativeTransferCallback?.Invoke(e);
        }

        private void _SetupMemXferEvent(object sender,TwainX.SetupMemXferEventArgs e) {
            this.SetupMemoryTransferCallback?.Invoke(e);
        }

        private void _MemXferEvent(object sender,TwainX.MemXferEventArgs e) {
            this.MemoryTransferCallback?.Invoke(e);
        }

        private void _SetupFileXferEvent(object sender,TwainX.SetupFileXferEventArgs e) {
            this.SetupFileTransferCallback?.Invoke(e);
        }

        private void _FileXferEvent(object sender,TwainX.FileXferEventArgs e) {
            this.FileTransferCallback?.Invoke(e);
        }

        private void _XferDone(object sender,TwainX.XferDoneEventArgs e) {
            this.ImageInfoCallback?.Invoke(e);
        }

        private void _AcquireCompleted(object sender,EventArgs e) {
            this.AcquireCompletedCallback?.Invoke();
        }

        private void _AcquireError(object sender,TwainX.AcquireErrorEventArgs e) {
            this.AcquireErrorCallback?.Invoke(e);
        }

        #endregion

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">Значение true позволяет освободить управляемые и неуправляемые ресурсы; значение false позволяет освободить только неуправляемые ресурсы.</param>
        protected override void Dispose(bool disposing) {
            if(disposing) {
                this.NativeTransferCallback=null;
                this.SetupMemoryTransferCallback=null;
                this.MemoryTransferCallback=null;
                this.SetupFileTransferCallback=null;
                this.FileTransferCallback=null;
                this.ImageInfoCallback=null;
                this.AcquireCompletedCallback=null;
                this.AcquireErrorCallback=null;
                if(this._isOwns) {
                    this.TwainX?.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the Data Sources.
        /// </summary>
        /// <value>
        /// The Sata Sources.
        /// </value>
        public IEnumerable<XDs> DataSources {
            get {
                for(var i = 0; i<this.TwainX.SourcesCount; i++) {
                    yield return XDs.Create(this,i);
                }
                yield break;
            }
        }

        /// <summary>
        /// Gets instance of the TwainX class.
        /// </summary>
        /// <value>
        /// The TwainX class.
        /// </value>
        /// <exception cref="System.InvalidOperationException">DSM don't open.</exception>
        public TwainX TwainX {
            get {
                if(!this._twainX.OpenDSM()) {
                    throw new InvalidOperationException("DSM don't open.");
                }
                return this._twainX;
            }
        }

        #region Callback Handlers

        internal Action<TwainX.EndXferEventArgs> NativeTransferCallback {
            get;
            set;
        }

        internal Action<TwainX.SetupMemXferEventArgs> SetupMemoryTransferCallback {
            get;
            set;
        }

        internal Action<TwainX.MemXferEventArgs> MemoryTransferCallback {
            get;
            set;
        }

        internal Action<TwainX.SetupFileXferEventArgs> SetupFileTransferCallback {
            get;
            set;
        }

        internal Action<TwainX.FileXferEventArgs> FileTransferCallback {
            get;
            set;
        }

        internal Action<TwainX.XferDoneEventArgs> ImageInfoCallback {
            get;
            set;
        }

        internal Action AcquireCompletedCallback {
            get;
            set;
        }

        internal Action<TwainX.AcquireErrorEventArgs> AcquireErrorCallback {
            get;
            set;
        }

        #endregion
    }
}
