using NUnit.Framework;
using Sharpen;

namespace Org.Apache.Hadoop.Security.Authentication.Util
{
	public class TestRolloverSignerSecretProvider
	{
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestGetAndRollSecrets()
		{
			long rolloverFrequency = 15 * 1000;
			// rollover every 15 sec
			byte[] secret1 = Sharpen.Runtime.GetBytesForString("doctor");
			byte[] secret2 = Sharpen.Runtime.GetBytesForString("who");
			byte[] secret3 = Sharpen.Runtime.GetBytesForString("tardis");
			TestRolloverSignerSecretProvider.TRolloverSignerSecretProvider secretProvider = new 
				TestRolloverSignerSecretProvider.TRolloverSignerSecretProvider(this, new byte[][]
				 { secret1, secret2, secret3 });
			try
			{
				secretProvider.Init(null, null, rolloverFrequency);
				byte[] currentSecret = secretProvider.GetCurrentSecret();
				byte[][] allSecrets = secretProvider.GetAllSecrets();
				Assert.AssertArrayEquals(secret1, currentSecret);
				NUnit.Framework.Assert.AreEqual(2, allSecrets.Length);
				Assert.AssertArrayEquals(secret1, allSecrets[0]);
				NUnit.Framework.Assert.IsNull(allSecrets[1]);
				Sharpen.Thread.Sleep(rolloverFrequency + 2000);
				currentSecret = secretProvider.GetCurrentSecret();
				allSecrets = secretProvider.GetAllSecrets();
				Assert.AssertArrayEquals(secret2, currentSecret);
				NUnit.Framework.Assert.AreEqual(2, allSecrets.Length);
				Assert.AssertArrayEquals(secret2, allSecrets[0]);
				Assert.AssertArrayEquals(secret1, allSecrets[1]);
				Sharpen.Thread.Sleep(rolloverFrequency + 2000);
				currentSecret = secretProvider.GetCurrentSecret();
				allSecrets = secretProvider.GetAllSecrets();
				Assert.AssertArrayEquals(secret3, currentSecret);
				NUnit.Framework.Assert.AreEqual(2, allSecrets.Length);
				Assert.AssertArrayEquals(secret3, allSecrets[0]);
				Assert.AssertArrayEquals(secret2, allSecrets[1]);
				Sharpen.Thread.Sleep(rolloverFrequency + 2000);
			}
			finally
			{
				secretProvider.Destroy();
			}
		}

		internal class TRolloverSignerSecretProvider : RolloverSignerSecretProvider
		{
			private byte[][] newSecretSequence;

			private int newSecretSequenceIndex;

			/// <exception cref="System.Exception"/>
			public TRolloverSignerSecretProvider(TestRolloverSignerSecretProvider _enclosing, 
				byte[][] newSecretSequence)
				: base()
			{
				this._enclosing = _enclosing;
				this.newSecretSequence = newSecretSequence;
				this.newSecretSequenceIndex = 0;
			}

			protected internal override byte[] GenerateNewSecret()
			{
				return this.newSecretSequence[this.newSecretSequenceIndex++];
			}

			private readonly TestRolloverSignerSecretProvider _enclosing;
		}
	}
}