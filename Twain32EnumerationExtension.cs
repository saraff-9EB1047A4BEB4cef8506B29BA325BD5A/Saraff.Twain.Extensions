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

    internal static class Twain32EnumerationExtension {

        internal static Twain32.Enumeration ToEnumeration(this object value) {
            if(value is Twain32.Range) {
                return Twain32.Enumeration.FromRange((Twain32.Range)value);
            }
            if(value is object[]) {
                return Twain32.Enumeration.FromArray((object[])value);
            }
            if(value is ValueType) {
                return Twain32.Enumeration.FromOneValue((ValueType)value);
            }
            if(value is string) {
                return Twain32.Enumeration.CreateEnumeration(new object[] { value },0,0);
            }
            return value as Twain32.Enumeration;
        }

        internal static Twain32.Enumeration ToEnumeration<T>(this object value) {
            Twain32.Enumeration _val = value.ToEnumeration();
            var _result = new object[_val.Count];
            for(int i = 0; i<_val.Count; i++) {
                _result[i]=typeof(T).IsEnum ? (T)_val[i] : Convert.ChangeType(_val[i],typeof(T));
            }
            return Twain32.Enumeration.CreateEnumeration(_result,_val.CurrentIndex,_val.DefaultIndex);
        }
    }
}
