using System;
using FluentAssertions;
using NUnit.Framework;

namespace Cerulean.Consul.Tests.WebExtensions
{
    [TestFixture]
    public class QueryTests
    {
        [Test]
        public void ToQueryStringShouldReturnEmptyStringWhenEmptyCollection()
        {
            // Arrange
            Query query = new Query();

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().BeEmpty();
        }

        [Test]
        public void ToQueryStringShouldReturnQueryStringWhenSingleValueCollection()
        {
            // Arrange
            Query query = new Query
            {
                {"flags", 42}
            };

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }

        [Test]
        public void ToQueryStringShouldReturnConcatenatedValuesWhenMultipleValueCollection()
        {
            // Arrange
            Query query = new Query
            {
                {"flags", 42},
                {"foo", "bar"}
            };

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42&foo=bar");
        }

        [Test]
        public void ToQueryStringShouldReturnUnaryValueWhenNameHasEmptyValue()
        {
            // Arrange
            Query query = new Query
            {
                {"recurse", ""}
            };

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().Be("recurse");
        }

        [Test]
        public void ToQueryStringShouldReturnUnaryValueWhenNameHasNullValue()
        {
            // Arrange
            Query query = new Query
            {
                {"recurse", null}
            };

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().Be("recurse");
        }

        [Test]
        public void ToQueryStringShouldIgnoreEmptyNames()
        {
            // Arrange
            Query query = new Query
            {
                {"flags", 42},
                {"", "a"}
            };

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }

        [Test, Ignore("Dictionary does not allow null key")]
        public void ToQueryStringShouldIgnoreNullNames()
        {
            // Arrange
            Query query = new Query
            {
                {"flags", 42},
                {null, "a"}
            };

            // Act
            string queryString = query.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }
    }
}