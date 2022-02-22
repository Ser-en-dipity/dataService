using System;
using System.Collections.Generic;

namespace Services{
    public record EsRecord {
        internal EsRecord() { }
        public EsRecord(Guid id, DateTime lastModifiedTime) => (Id, LastModifiedTime) = (id, lastModifiedTime);
        [Nest.PropertyName("id")]
        public Guid Id { get; init; }
        [Nest.PropertyName("last_modified_time")]
        public DateTime LastModifiedTime { get; init; }
    }
    public record SearchResponseRecord {
        public SearchResponseRecord(Guid id, double score)
        {
            (Id, Score) = (id, score);
        }

        public Guid Id { get; init; }

        public double Score { get; init; }
    }
    public interface IElasticSearchService {
        IEnumerable<SearchResponseRecord> SearchTools(string query);
        IEnumerable<SearchResponseRecord> Search<T>(string query);
    }
}