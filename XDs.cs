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
using System.Drawing;
using System.Linq;
using System.Text;

namespace Saraff.TwainX.Extensions {

    /// <summary>
    /// Represent a Data Source of a TWAIN.
    /// </summary>
    public sealed class XDs {
        private int _index = -1;

        private XDs() {
        }

        /// <summary>
        /// Creates the specified DS.
        /// </summary>
        /// <param name="dsm">The DSM.</param>
        /// <param name="index">The index of a DS.</param>
        /// <returns>Instance of a XDs class.</returns>
        public static XDs Create(XDsm dsm,int index) {
            return new Extensions.XDs {
                Dsm=dsm,
                _index=index
            };
        }

        /// <summary>
        /// Gets the capability.
        /// </summary>
        /// <typeparam name="T">Type of a capability value.</typeparam>
        /// <param name="capability">The capability.</param>
        /// <returns>Instance of XDs class.</returns>
        public XCapability<T> GetCapability<T>(TwCap capability) {
            return XCapability<T>.Create(this,capability);
        }

        /// <summary>
        /// Begins acquire image using a Natives transfer mechanism.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="imageInfoCallback">The image information callback.</param>
        /// <param name="completeCallback">The complete callback.</param>
        /// <param name="errorCallback">The error callback.</param>
        public void NativeTransfer(Action<TwainX.EndXferEventArgs> result,Action<TwainX.XferDoneEventArgs> imageInfoCallback = null,Action completeCallback = null,Action<TwainX.AcquireErrorEventArgs> errorCallback = null) {
            this.TwainX.Capabilities.XferMech.Set(TwSX.Native);
            this.Dsm.NativeTransferCallback=result;
            this.Dsm.ImageInfoCallback=imageInfoCallback;
            this.Dsm.AcquireCompletedCallback=completeCallback;
            this.Dsm.AcquireErrorCallback=errorCallback;
            this.TwainX.Acquire();
        }

        /// <summary>
        /// Begins acquire image using a Buffered Memory Transfer mechanism.
        /// </summary>
        /// <param name="setupCallback">The setup callback.</param>
        /// <param name="transferCallback">The transfer callback.</param>
        /// <param name="imageInfoCallback">The image information callback.</param>
        /// <param name="completeCallback">The complete callback.</param>
        /// <param name="errorCallback">The error callback.</param>
        public void MemoryTransfer(Action<TwainX.SetupMemXferEventArgs> setupCallback,Action<TwainX.MemXferEventArgs> transferCallback,Action<TwainX.XferDoneEventArgs> imageInfoCallback = null,Action completeCallback = null,Action<TwainX.AcquireErrorEventArgs> errorCallback = null) {
            this.TwainX.Capabilities.XferMech.Set(TwSX.Memory);
            this.Dsm.SetupMemoryTransferCallback=setupCallback;
            this.Dsm.MemoryTransferCallback=transferCallback;
            this.Dsm.ImageInfoCallback=imageInfoCallback;
            this.Dsm.AcquireCompletedCallback=completeCallback;
            this.Dsm.AcquireErrorCallback=errorCallback;
            this.TwainX.Acquire();
        }

        /// <summary>
        /// Begins acquire image using a Buffered Memory Transfer mechanism With File Format.
        /// </summary>
        /// <param name="setupCallback">The setup callback.</param>
        /// <param name="transferCallback">The transfer callback.</param>
        /// <param name="imageInfoCallback">The image information callback.</param>
        /// <param name="completeCallback">The complete callback.</param>
        /// <param name="errorCallback">The error callback.</param>
        public void MemFileTransfer(Action<TwainX.SetupMemXferEventArgs> setupCallback,Action<TwainX.MemXferEventArgs> transferCallback,Action<TwainX.XferDoneEventArgs> imageInfoCallback = null,Action completeCallback = null,Action<TwainX.AcquireErrorEventArgs> errorCallback = null) {
            this.TwainX.Capabilities.XferMech.Set(TwSX.MemFile);
            this.Dsm.SetupMemoryTransferCallback=setupCallback;
            this.Dsm.MemoryTransferCallback=transferCallback;
            this.Dsm.ImageInfoCallback=imageInfoCallback;
            this.Dsm.AcquireCompletedCallback=completeCallback;
            this.Dsm.AcquireErrorCallback=errorCallback;
            this.TwainX.Acquire();
        }

        /// <summary>
        /// Begins acquire image using a Files Transfer mechanism.
        /// </summary>
        /// <param name="setupCallback">The setup callback.</param>
        /// <param name="transferCallback">The transfer callback.</param>
        /// <param name="imageInfoCallback">The image information callback.</param>
        /// <param name="completeCallback">The complete callback.</param>
        /// <param name="errorCallback">The error callback.</param>
        public void FileTransfer(Action<TwainX.SetupFileXferEventArgs> setupCallback,Action<TwainX.FileXferEventArgs> transferCallback,Action<TwainX.XferDoneEventArgs> imageInfoCallback = null,Action completeCallback = null,Action<TwainX.AcquireErrorEventArgs> errorCallback = null) {
            this.TwainX.Capabilities.XferMech.Set(TwSX.File);
            this.Dsm.SetupFileTransferCallback=setupCallback;
            this.Dsm.FileTransferCallback=transferCallback;
            this.Dsm.ImageInfoCallback=imageInfoCallback;
            this.Dsm.AcquireCompletedCallback=completeCallback;
            this.Dsm.AcquireErrorCallback=errorCallback;
            this.TwainX.Acquire();
        }

        /// <summary>
        /// Gets a value indicating whether this DS is default.
        /// </summary>
        /// <value>
        /// <c>true</c> if this DS is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault {
            get {
                return this.Dsm.TwainX.GetDefaultSource()==this._index;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this DS is TWAIN2.x compatible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this DS is TWAIN2.x compatible; otherwise, <c>false</c>.
        /// </value>
        public bool IsTwain2Compatible {
            get {
                return this.Dsm.TwainX.GetIsSourceTwain2Compatible(this._index);
            }
        }

        /// <summary>
        /// Gets the identity of a Data Source.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public TwainX.Identity Identity {
            get {
                return this.Dsm.TwainX.GetSourceIdentity(this._index);
            }
        }

        /// <summary>
        /// Gets the DSM.
        /// </summary>
        /// <value>
        /// The DSM.
        /// </value>
        public XDsm Dsm {
            get;
            private set;
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
                if(this.Dsm.TwainX.SourceIndex!=this._index) {
                    this.Dsm.TwainX.CloseDataSource();
                    this.Dsm.TwainX.SourceIndex=this._index;
                }
                this.Dsm.TwainX.OpenDataSource();
                return this.Dsm.TwainX;
            }
        }
    }
}
