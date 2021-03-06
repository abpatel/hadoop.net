using System.Collections.Generic;
using NUnit.Framework;
using Org.Apache.Hadoop.Conf;


namespace Org.Apache.Hadoop.Net
{
	public class TestScriptBasedMappingWithDependency : TestCase
	{
		public TestScriptBasedMappingWithDependency()
		{
		}

		[Fact]
		public virtual void TestNoArgsMeansNoResult()
		{
			Configuration conf = new Configuration();
			conf.SetInt(ScriptBasedMapping.ScriptArgCountKey, ScriptBasedMapping.MinAllowableArgs
				 - 1);
			conf.Set(ScriptBasedMapping.ScriptFilenameKey, "any-filename-1");
			conf.Set(ScriptBasedMappingWithDependency.DependencyScriptFilenameKey, "any-filename-2"
				);
			conf.SetInt(ScriptBasedMapping.ScriptArgCountKey, 10);
			ScriptBasedMappingWithDependency mapping = CreateMapping(conf);
			IList<string> names = new AList<string>();
			names.AddItem("some.machine.name");
			names.AddItem("other.machine.name");
			IList<string> result = mapping.Resolve(names);
			NUnit.Framework.Assert.IsNull("Expected an empty list for resolve", result);
			result = mapping.GetDependency("some.machine.name");
			NUnit.Framework.Assert.IsNull("Expected an empty list for getDependency", result);
		}

		/// <exception cref="System.Exception"/>
		[Fact]
		public virtual void TestNoFilenameMeansSingleSwitch()
		{
			Configuration conf = new Configuration();
			ScriptBasedMapping mapping = CreateMapping(conf);
			Assert.True("Expected to be single switch", mapping.IsSingleSwitch
				());
			Assert.True("Expected to be single switch", AbstractDNSToSwitchMapping
				.IsMappingSingleSwitch(mapping));
		}

		/// <exception cref="System.Exception"/>
		[Fact]
		public virtual void TestFilenameMeansMultiSwitch()
		{
			Configuration conf = new Configuration();
			conf.Set(ScriptBasedMapping.ScriptFilenameKey, "any-filename");
			ScriptBasedMapping mapping = CreateMapping(conf);
			NUnit.Framework.Assert.IsFalse("Expected to be multi switch", mapping.IsSingleSwitch
				());
			mapping.SetConf(new Configuration());
			Assert.True("Expected to be single switch", mapping.IsSingleSwitch
				());
		}

		/// <exception cref="System.Exception"/>
		[Fact]
		public virtual void TestNullConfig()
		{
			ScriptBasedMapping mapping = CreateMapping(null);
			Assert.True("Expected to be single switch", mapping.IsSingleSwitch
				());
		}

		private ScriptBasedMappingWithDependency CreateMapping(Configuration conf)
		{
			ScriptBasedMappingWithDependency mapping = new ScriptBasedMappingWithDependency();
			mapping.SetConf(conf);
			return mapping;
		}
	}
}
