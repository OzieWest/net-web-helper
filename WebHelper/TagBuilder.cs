using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper
{
    public abstract class BaseTagBuilder
    {
        protected StringBuilder htmlBuilder = new StringBuilder();

        /// <summary>
        /// Возвращает сформированную структуру
        /// </summary>
        public virtual string Render()
        {
            var _result = this.htmlBuilder.ToString();
            this.htmlBuilder.Clear();
            return _result;
        }

        /// <summary>
        /// Формирует ТЕГ
        /// </summary>
        /// <param name="name">Имя тега</param>
        /// <param name="selfContained">Является ли тег одиночным</param>
        /// <param name="withAttributes">Объект атрибутов</param>
        /// <param name="withChildren">Метод для формирования вложенных тегов</param>
        public virtual void Tag(string name, bool selfContained = false, object withAttributes = null, Action withChildren = null)
        {
            this.htmlBuilder.Append("<" + name);

            if (withAttributes != null)
            {
                string[] _propertyNames = withAttributes.GetType().GetProperties().Select(p => p.Name).ToArray();
                foreach (var _prop in _propertyNames)
                {
                    object _propValue = withAttributes.GetType().GetProperty(_prop).GetValue(withAttributes, null);

                    AddAtribute(_prop, _propValue.ToString());
                }
            }

            if (selfContained)
            {
                this.htmlBuilder.Append("/>");
                return;
            }
            else
            {
                this.htmlBuilder.Append(">");
         
                if (withChildren != null)
                    withChildren();

                this.htmlBuilder.Append("</" + name + ">");
            }
        }

        /// <summary>
        /// Добавляет атрибуты текущему тегу
        /// </summary>
        /// <param name="name">Имя атрибута</param>
        /// <param name="value">Значение атрибута</param>
        /// <param name="withOffset">Флаг - наличие отступа в строке</param>
        protected virtual void AddAtribute(string name, string value, bool withOffset = true, bool withQuotes = true)
        {
            // TODO - bool withOffset = true, bool withQuotes = true
            String _result = null;

            if (!String.IsNullOrEmpty(name)) 
            {
                name = String.Concat(name.Select(x => Char.IsUpper(x) ? "-" + Char.ToLower(x) : x.ToString())).TrimStart(' ');

                if (!String.IsNullOrEmpty(value))
                {
                    _result = String.Format(" {0}='{1}'", name, value);
                }
                else
                {
                    _result = String.Format(" {0}", name);
                }
            }

            this.htmlBuilder.Append(_result);
        }
    }

    public class SimpleTagBuilder : BaseTagBuilder, IDisposable
    {
        public virtual SimpleTagBuilder Text(string text)
        {
            this.htmlBuilder.Append(text);
            return this;
        }

        public void Dispose()
        {
            this.htmlBuilder = null;
        }
    }
}
