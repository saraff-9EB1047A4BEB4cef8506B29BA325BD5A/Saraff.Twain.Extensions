/* Этот файл является частью библиотеки Saraff.Twain.Extensions
 * © SARAFF SOFTWARE (Кирножицкий Андрей), 2017.
 * Saraff.Twain.Extensions - свободная программа: вы можете перераспространять ее и/или
 * изменять ее на условиях Меньшей Стандартной общественной лицензии GNU в том виде,
 * в каком она была опубликована Фондом свободного программного обеспечения;
 * либо версии 3 лицензии, либо (по вашему выбору) любой более поздней
 * версии.
 * Saraff.Twain.Extensions распространяется в надежде, что она будет полезной,
 * но БЕЗО ВСЯКИХ ГАРАНТИЙ; даже без неявной гарантии ТОВАРНОГО ВИДА
 * или ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. Подробнее см. в Меньшей Стандартной
 * общественной лицензии GNU.
 * Вы должны были получить копию Меньшей Стандартной общественной лицензии GNU
 * вместе с этой программой. Если это не так, см.
 * <http://www.gnu.org/licenses/>.)
 * 
 * This file is part of Saraff.Twain.Extensions.
 * © SARAFF SOFTWARE (Kirnazhytski Andrei), 2017.
 * Saraff.Twain.Extensions is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * Saraff.Twain.Extensions is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public License
 * along with Saraff.Twain.Extensions. If not, see <http://www.gnu.org/licenses/>.
 * 
 * PLEASE SEND EMAIL TO:  twain@saraff.ru.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Saraff.Twain.Extensions {

    /// <summary>
    /// Represent a Data Source Manager of a TWAIN.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    public sealed class XDsm:Component {
        private bool _isOwns=false;
        private Twain32 _twain32 = null;

        private XDsm() {
        }

        /// <summary>
        /// Create instance of a XDsm class.
        /// </summary>
        /// <param name="isTwain2Enable">if set to <c>true</c> is a TWAIN2.x enable.</param>
        /// <param name="isShowUI">if set to <c>true</c> is show UI of a Data Source.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        public static XDsm Create(bool isTwain2Enable = false,bool isShowUI = true,TwLanguage language = TwLanguage.RUSSIAN,TwCountry country = TwCountry.BELARUS) {
            return XDsm.Create(
                new Twain32 {
                    IsTwain2Enable=isTwain2Enable,
                    ShowUI=isShowUI,
                    Language=language,
                    Country=country
                },
                true);
        }

        /// <summary>
        /// Create instance of a XDsm class.
        /// </summary>
        /// <param name="twain32">Instance of the Twain32 class.</param>
        /// <param name="isOwns">If set to <c>true</c> is owner of instance of the Twain32 class.</param>
        /// <returns></returns>
        public static XDsm Create(Twain32 twain32,bool isOwns = false) {
            XDsm _dsm = null;
            try {
                return _dsm=new XDsm {
                    _twain32=twain32,
                    _isOwns=isOwns
                };
            } finally {
                twain32.EndXfer+=_dsm._EndXfer;
                twain32.XferDone+=_dsm._XferDone;
                twain32.SetupMemXferEvent+=_dsm._SetupMemXferEvent;
                twain32.MemXferEvent+=_dsm._MemXferEvent;
                twain32.SetupFileXferEvent+=_dsm._SetupFileXferEvent;
                twain32.FileXferEvent+=_dsm._FileXferEvent;
                twain32.AcquireCompleted+=_dsm._AcquireCompleted;
                twain32.AcquireError+=_dsm._AcquireError;
            }
        }

        #region Events Handlers

        private void _EndXfer(object sender,Twain32.EndXferEventArgs e) {
            this.NativeTransferCallback?.Invoke(e);
        }

        private void _SetupMemXferEvent(object sender,Twain32.SetupMemXferEventArgs e) {
            this.SetupMemoryTransferCallback?.Invoke(e);
        }

        private void _MemXferEvent(object sender,Twain32.MemXferEventArgs e) {
            this.MemoryTransferCallback?.Invoke(e);
        }

        private void _SetupFileXferEvent(object sender,Twain32.SetupFileXferEventArgs e) {
            this.SetupFileTransferCallback?.Invoke(e);
        }

        private void _FileXferEvent(object sender,Twain32.FileXferEventArgs e) {
            this.FileTransferCallback?.Invoke(e);
        }

        private void _XferDone(object sender,Twain32.XferDoneEventArgs e) {
            this.ImageInfoCallback?.Invoke(e);
        }

        private void _AcquireCompleted(object sender,EventArgs e) {
            this.AcquireCompletedCallback?.Invoke();
        }

        private void _AcquireError(object sender,Twain32.AcquireErrorEventArgs e) {
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
                    this.Twain32?.Dispose();
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
                for(var i = 0; i<this.Twain32.SourcesCount; i++) {
                    yield return XDs.Create(this,i);
                }
                yield break;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is TWAIN2.x supported.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is TWAIN2.x supported; otherwise, <c>false</c>.
        /// </value>
        public bool IsTwain2Supported {
            get {
                return this.Twain32.IsTwain2Supported;
            }
        }

        /// <summary>
        /// Gets instance of the Twain32 class.
        /// </summary>
        /// <value>
        /// The Twain32 class.
        /// </value>
        /// <exception cref="System.InvalidOperationException">DSM don't open.</exception>
        public Twain32 Twain32 {
            get {
                if(!this._twain32.OpenDSM()) {
                    throw new InvalidOperationException("DSM don't open.");
                }
                return this._twain32;
            }
        }

        #region Callback Handlers

        internal Action<Twain32.EndXferEventArgs> NativeTransferCallback {
            get;
            set;
        }

        internal Action<Twain32.SetupMemXferEventArgs> SetupMemoryTransferCallback {
            get;
            set;
        }

        internal Action<Twain32.MemXferEventArgs> MemoryTransferCallback {
            get;
            set;
        }

        internal Action<Twain32.SetupFileXferEventArgs> SetupFileTransferCallback {
            get;
            set;
        }

        internal Action<Twain32.FileXferEventArgs> FileTransferCallback {
            get;
            set;
        }

        internal Action<Twain32.XferDoneEventArgs> ImageInfoCallback {
            get;
            set;
        }

        internal Action AcquireCompletedCallback {
            get;
            set;
        }

        internal Action<Twain32.AcquireErrorEventArgs> AcquireErrorCallback {
            get;
            set;
        }

        #endregion
    }
}
