using Google.Protobuf.Reflection;
using System;
using System.IO;

namespace Protobuf2UmlLibrary
{
	public class Class1
	{

		private void Test()
		{
			using (var stream = File.OpenRead(@"C:\Users\Agando\Desktop\protoc-3.9.1-win64\bin\grpcserverschema"))
			{
				FileDescriptorSet descriptorSet = FileDescriptorSet.Parser.ParseFrom(stream);
				Google.Protobuf.Collections.RepeatedField<FileDescriptorProto> descriptorsProto = descriptorSet.File;
			}
		}
	}
}
