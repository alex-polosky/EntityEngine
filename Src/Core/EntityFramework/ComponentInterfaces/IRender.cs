using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework.AssetFileInterface;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class IRender : Component
    {
        public Guid ModelReference { get; set; }
        public Guid ShaderReference { get; set; }

        public IModel GetModel(IGuidManager guidManager)
        {
            return (IModel)guidManager.GetAssetFromGuid(ModelReference);
        }

        public IShader GetShader(IGuidManager guidManager)
        {
            return (IShader)guidManager.GetAssetFromGuid(ShaderReference);
        }

        public bool Active { get; set; }

        public IRender()
        {

        }
    }
}
