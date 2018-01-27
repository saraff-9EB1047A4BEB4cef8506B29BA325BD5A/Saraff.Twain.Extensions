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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saraff.TwainX.Extensions {

    public sealed class XCapability<T> {

        private XCapability() {
        }

        /// <summary>
        /// Creates the specified a Capability.
        /// </summary>
        /// <param name="ds">The DS.</param>
        /// <param name="cap">The Capability.</param>
        /// <returns></returns>
        public static XCapability<T> Create(XDs ds,TwCap cap) {
            return new XCapability<T> {
                DataSource=ds,
                Id=cap
            };
        }

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Set(T value) {
            this.TwainX.SetCap(this.Id,value);
        }

        /// <summary>
        /// Resets current value of capability to a default value.
        /// </summary>
        public void Reset() {
            this.TwainX.ResetCap(this.Id);
        }

        /// <summary>
        /// Gets the capability identifier.
        /// </summary>
        /// <value>
        /// The capability identifier.
        /// </value>
        public TwCap Id {
            get;
            private set;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IEnumerable<XCapabilityValue<T>> Values {
            get {
                var _values = this.TwainX.GetCap(this.Id).ToEnumeration<T>();
                for(var i = 0; i<_values.Count; i++) {
                    yield return XCapabilityValue<T>.Create(this,(T)_values[i]);
                }
                yield break;
            }
        }

        /// <summary>
        /// Gets supported operations.
        /// </summary>
        /// <value>
        /// Supported operations.
        /// </value>
        public TwQC IsSupported {
            get {
                return this.TwainX.IsCapSupported(this.Id);
            }
        }

        /// <summary>
        /// Gets the Data Source.
        /// </summary>
        /// <value>
        /// The Data Source.
        /// </value>
        public XDs DataSource {
            get;
            private set;
        }

        private TwainX TwainX {
            get {
                return this.DataSource.TwainX;
            }
        }
    }
}
