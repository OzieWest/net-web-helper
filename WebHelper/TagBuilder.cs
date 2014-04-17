using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper
{
    public abstract class BaseTagBuilder
    {
        protected StringBuilder _htmlBuilder = new StringBuilder();

        /// <summary>
        /// Возвращает сформированную структуру
        /// </summary>
        public virtual string Render()
        {
            var result = _htmlBuilder.ToString();
            _htmlBuilder.Clear();
            return result;
        }

        /// <summary>
        /// Формирует ТЕГ
        /// </summary>
        /// <param name="name">Имя тега</param>
        /// <param name="selfClose">Является ли тег одиночным</param>
        /// <param name="withAttributes">Объект атрибутов</param>
        /// <param name="withChildren">Метод для формирования вложенных тегов</param>
        public virtual void Tag(string name, bool selfClose = false, object withAttributes = null, Action withChildren = null)
        {
            _htmlBuilder.Append("<" + name);

            if (withAttributes != null)
            {
                string[] propertyNames = withAttributes.GetType().GetProperties().Select(p => p.Name).ToArray();
                foreach (var prop in propertyNames)
                {
                    object propValue = withAttributes.GetType().GetProperty(prop).GetValue(withAttributes, null);

                    AddAtribute(prop, propValue.ToString());
                }
            }

            if (selfClose)
            {
                _htmlBuilder.Append("/>");
                return;
            }
            else
            {
                _htmlBuilder.Append(">");
         
                if (withChildren != null)
                    withChildren();

                _htmlBuilder.Append("</" + name + ">");
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
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(value))
            {
                var result = String.Format(" {0}='{1}'", name, value);
                _htmlBuilder.Append(result);
            }
        }
    }

    public class SimpleTagBuilder : BaseTagBuilder, IDisposable
    {
        public virtual SimpleTagBuilder Text(string text)
        {
            _htmlBuilder.Append(text);
            return this;
        }

        public void Dispose()
        {
            _htmlBuilder = null;
        }
    }
}
