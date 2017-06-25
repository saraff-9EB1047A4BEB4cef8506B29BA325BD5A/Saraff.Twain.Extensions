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
using System.Linq;
using System.Text;

namespace Saraff.Twain.Extensions {

    public sealed class XCapabilityValue<T> {

        private XCapabilityValue() {
        }

        /// <summary>
        /// Creates the specified a Capability value.
        /// </summary>
        /// <param name="cap">The capability.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static XCapabilityValue<T> Create(XCapability<T> cap,T value) {
            return new Extensions.XCapabilityValue<T> {
                Capability=cap,
                Value=value
            };
        }

        /// <summary>
        /// Sets this value of a capability as current.
        /// </summary>
        public void Set() {
            this.Capability.Set(this.Value);
        }

        /// <summary>
        /// Gets a value indicating whether this value of a capability is current.
        /// </summary>
        /// <value>
        /// <c>true</c> if this value of a capability is current; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrent {
            get {
                return this.Value.Equals((T)this.Twain32.GetCurrentCap(this.Capability.Id));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this value of a capability is default.
        /// </summary>
        /// <value>
        /// <c>true</c> if this value of a capability is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault {
            get {
                return this.Value.Equals((T)this.Twain32.GetDefaultCap(this.Capability.Id));
            }
        }

        /// <summary>
        /// Gets the value of a Capability.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Capability.
        /// </summary>
        /// <value>
        /// The capability.
        /// </value>
        public XCapability<T> Capability {
            get;
            private set;
        }

        private Twain32 Twain32 {
            get {
                return this.Capability.DataSource.Twain32;
            }
        }
    }
}
