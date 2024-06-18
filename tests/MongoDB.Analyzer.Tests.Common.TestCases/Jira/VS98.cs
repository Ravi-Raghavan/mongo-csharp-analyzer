// Copyright 2021-present MongoDB Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using MongoDB.Analyzer.Tests.Common.DataModel;

// DO NOT INCLUDE 'MongoDB.Driver.Linq' USING IN THIS FILE.
// The tests test IMongoQueryable type inference without direct MongoDB.Driver.Linq reference.


namespace MongoDB.Analyzer.Tests.Common.TestCases.Jira
{
    internal sealed class VS98 : TestCasesBase
    {
        [PocoJson("{ \"list1\" : [], \"list2\" : [], \"list3\" : [], \"list4\" : [], \"list5\" : [], \"dictionary1\" : { }, \"dictionary2\" : { }, \"dictionary3\" : { }, \"dictionary4\" : { }, \"dictionary5\" : null }")]
        public void DictionaryHolder()
        {

        }

        public class TestClasses
        {
            public class DictionaryHolder
            {
                public List<int> list1;
                public IList<int> list2;
                public IList<IList<string>> list3;
                public IList<IList<IList<IList<string>>>> list4;
                public List<List<Dictionary<User, string>>> list5;
                public Dictionary<int, int> dictionary1;
                public Dictionary<string, int> dictionary2;
                public Dictionary<string, List<int>> dictionary3;
                public Dictionary<string, Dictionary<string, string>> dictionary4;
                public IDictionary<string, IDictionary<string, string>> dictionary5;
            }
        }
    }
}

