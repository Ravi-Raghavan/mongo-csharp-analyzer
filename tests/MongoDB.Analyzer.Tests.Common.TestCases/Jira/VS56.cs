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


using System.Linq;
using MongoDB.Analyzer.Tests.Common.DataModel;
using MongoDB.Driver;

// DO NOT INCLUDE 'MongoDB.Driver.Linq' USING IN THIS FILE.
// The tests test IMongoQueryable type inference without direct MongoDB.Driver.Linq reference.


namespace MongoDB.Analyzer.Tests.Common.TestCases.Jira
{
    internal sealed class VS56 : TestCasesBase
    {
        [BuildersMQL("{ Address : { $exists : false } } ")]
        public void Constant_values_replacement_functional_syntax()
        {
            int value = 10;
            _ = Builders<User>.Filter.Exists(u => u.Address, value == 10);
        }
    }
}

