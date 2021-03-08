using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //bu loglama içinmiş ama şu an hazır değilmiş kullanmıyormuşuz
            classAttributes.Add(new PerformanceAspect(1)); // 1 dk dan daha uzun çalışan metodu göster

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
