using System.Collections.Specialized;
using FluentAssertions;
using NUnit.Framework;
using Wildflower.Consul.WebExtensions;

namespace Wildflower.Consul.Tests.WebExtensions
{
    [TestFixture]
    public class NameValueCollectionExtensionsTests
    {
        [Test]
        public void ToQueryStringShouldReturnEmptyStringWhenEmptyCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection();

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().BeEmpty();
        }

        [Test]
        public void ToQueryStringShouldReturnQueryStringWhenSingleValueCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection
            {
                {"flags", "42"}
            };

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }

        [Test]
        public void ToQueryStringShouldReturnConcatenatedValuesWhenMultipleValueCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection
            {
                {"flags", "42"},
                {"foo", "bar"}
            };

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42&foo=bar");
        }

        [Test]
        public void ToQueryStringShouldReturnUnaryValueWhenNameHasEmptyValue()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection
            {
                {"recurse", ""}
            };

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().Be("recurse");
        }

        [Test]
        public void ToQueryStringShouldReturnUnaryValueWhenNameHasNullValue()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection
            {
                {"recurse", null}
            };

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().Be("recurse");
        }

        [Test]
        public void ToQueryStringShouldIgnoreEmptyNames()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection
            {
                {"flags", "42"},
                {"", "a"}
            };

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }

        [Test]
        public void ToQueryStringShouldIgnoreNullNames()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection
            {
                {"flags", "42"},
                {null, "a"}
            };

            // Act
            string queryString = collection.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }
    }
}