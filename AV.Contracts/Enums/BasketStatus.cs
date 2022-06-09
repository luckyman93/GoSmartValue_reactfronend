using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AV.Contracts.Exceptions;

namespace AV.Contracts.Enums
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }
    public class BasketStatus
        : Enumeration
    {
        public static BasketStatus Draft = new BasketStatus(1, nameof(Draft).ToLowerInvariant());
        public static BasketStatus Abandoned = new BasketStatus(2, nameof(Abandoned).ToLowerInvariant());
        public static BasketStatus Confirmed = new BasketStatus(3, nameof(Confirmed).ToLowerInvariant());
        public static BasketStatus Paid = new BasketStatus(4, nameof(Paid).ToLowerInvariant());
        public static BasketStatus Completed = new BasketStatus(5, nameof(Completed).ToLowerInvariant());
        public static BasketStatus Cancelled = new BasketStatus(6, nameof(Cancelled).ToLowerInvariant());

        public BasketStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<BasketStatus> List() =>
            new[] { Draft, Confirmed, Abandoned, Paid, Completed, Cancelled };

        public static BasketStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new BasketOrderException($"Possible values for BasketStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static BasketStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new BasketOrderException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
