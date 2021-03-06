

namespace Org.Apache.Hadoop.Security
{
	/// <summary>
	/// An interface for the implementation of <userId, userName> mapping
	/// and <groupId, groupName> mapping
	/// </summary>
	public interface IdMappingServiceProvider
	{
		// Return uid for given user name
		/// <exception cref="System.IO.IOException"/>
		int GetUid(string user);

		// Return gid for given group name
		/// <exception cref="System.IO.IOException"/>
		int GetGid(string group);

		// Return user name for given user id uid, if not found, return 
		// <unknown> passed to this method
		string GetUserName(int uid, string unknown);

		// Return group name for given groupd id gid, if not found, return 
		// <unknown> passed to this method
		string GetGroupName(int gid, string unknown);

		// Return uid for given user name.
		// When can't map user, return user name's string hashcode
		int GetUidAllowingUnknown(string user);

		// Return gid for given group name.
		// When can't map group, return group name's string hashcode
		int GetGidAllowingUnknown(string group);
	}
}
