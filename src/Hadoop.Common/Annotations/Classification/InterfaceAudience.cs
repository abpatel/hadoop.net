/*
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/


namespace Org.Apache.Hadoop.Classification
{
	/// <summary>Annotation to inform users of a package, class or method's intended audience.
	/// 	</summary>
	/// <remarks>
	/// Annotation to inform users of a package, class or method's intended audience.
	/// Currently the audience can be
	/// <see cref="Public"/>
	/// ,
	/// <see cref="LimitedPrivate"/>
	/// or
	/// <see cref="Private"/>
	/// . <br />
	/// All public classes must have InterfaceAudience annotation. <br />
	/// <ul>
	/// <li>Public classes that are not marked with this annotation must be
	/// considered by default as
	/// <see cref="Private"/>
	/// .</li>
	/// <li>External applications must only use classes that are marked
	/// <see cref="Public"/>
	/// . Avoid using non public classes as these classes
	/// could be removed or change in incompatible ways.</li>
	/// <li>Hadoop projects must only use classes that are marked
	/// <see cref="LimitedPrivate"/>
	/// or
	/// <see cref="Public"/>
	/// </li>
	/// <li> Methods may have a different annotation that it is more restrictive
	/// compared to the audience classification of the class. Example: A class
	/// might be
	/// <see cref="Public"/>
	/// , but a method may be
	/// <see cref="LimitedPrivate"/>
	/// </li></ul>
	/// </remarks>
	public class InterfaceAudience
	{
		private InterfaceAudience()
		{
		}
		// Audience can't exist on its own
	}
}
