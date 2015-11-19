namespace MovieHunter.Tests.TestObjects
{
    using System.Security.Principal;

    public class MockedPrincipal : IPrincipal
    {
        public IIdentity Identity
        {
            get
            {
                return new MockedIIdentity();
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
