using System;
using System.Linq;
using dnlib.DotNet;

namespace NETReactorSlayer.De4dot
{
    public abstract class DeobfuscatorBase : IDeobfuscator
    {
        protected DeobfuscatorBase(ModuleDefMD module, OptionsBase optionsBase)
        {
            _optionsBase = optionsBase;
            Module = module;
        }

        protected virtual bool CheckValidName(string name) => _optionsBase.ValidNameRegex.IsMatch(name);

        public override string ToString() => Name;

        protected virtual void Dispose(bool disposing)
        {
        }

        public virtual bool IsValidNamespaceName(string ns) => ns != null && ns.Split('.').All(CheckValidName);

        public virtual bool IsValidTypeName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidMethodName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidPropertyName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidEventName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidFieldName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidGenericParamName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidMethodArgName(string name) => name != null && CheckValidName(name);

        public virtual bool IsValidMethodReturnArgName(string name) =>
            string.IsNullOrEmpty(name) || CheckValidName(name);

        public virtual bool IsValidResourceKeyName(string name) => name != null && CheckValidName(name);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private readonly OptionsBase _optionsBase;
        protected ModuleDefMD Module;

        public const string DefaultAsianValidNameRegex =
            @"^[\u2E80-\u9FFFa-zA-Z_<{$][\u2E80-\u9FFFa-zA-Z_0-9<>{}$.`-]*$";

        public const string DefaultValidNameRegex = @"^[a-zA-Z_<{$][a-zA-Z_0-9<>{}$.`-]*$";
        public abstract string Name { get; }
        public virtual RenamingOptions RenamingOptions { get; set; }

        public IDeobfuscatorOptions TheOptions => _optionsBase;

        public class OptionsBase : IDeobfuscatorOptions
        {
            public OptionsBase() => RenameResourcesInCode = true;

            public bool RenameResourcesInCode { get; set; }

            public NameRegexes ValidNameRegex { get; set; }
        }
    }
}