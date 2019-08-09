using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Xunit;

namespace dotnet_validate_json_with_schema
{
    public class PersonModel {
        public string name;
    }

    public class MyTests
    {
        [Fact]
        public void PersonIsJames()
        {
            string rawJsonPerson = @"{
                'name': 'imkk-000'
            }";
            var person = JObject.Parse(rawJsonPerson);

            Assert.True(person.Validate(PersonSchema));
            Assert.Equal("imkk-000", person.GetValue("name").ToObject<string>());
        }

        public JSchema PersonSchema() => JSchema.Parse(@"{
            'properties': {
                'name': { 'type': 'string' }
            }
        }");
    }

    public static class JObjectExtension {
        public static bool Validate(this JObject jObj, Func<JSchema> schemaMethod) => jObj.IsValid(schemaMethod.Invoke());
    }
}
