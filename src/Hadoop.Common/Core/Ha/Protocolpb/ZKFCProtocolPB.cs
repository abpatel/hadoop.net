using Org.Apache.Hadoop.HA.Proto;
using Org.Apache.Hadoop.Ipc;


namespace Org.Apache.Hadoop.HA.ProtocolPB
{
	public interface ZKFCProtocolPB : ZKFCProtocolProtos.ZKFCProtocolService.BlockingInterface
		, VersionedProtocol
	{
	}
}
