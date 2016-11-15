using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace EntityFramework.Test.EntityEngine.Entity
{
    [TestFixture]
    public class ComponentSystems
    {
        private EntityFramework.IGuidManager _guids;
        private EntityFramework.ISystemManager _system;
        private EntityFramework.Entity _entity;

        // These methods are not TestCaseSetups because
        //  I need to test these cases in every tc, 
        //  but they still need to be tested
        private void TestSetUp()
        {
            _guids = new EntityFramework.Manager.GuidManager();
            _system = new EntityFramework.Manager.SystemManager();
            // I know, I should test the new guid system before actually
            //  calling this, however there's really no other way to set
            //  this up properly. If anything, NewGuid() should return
            //  a blank one. If not.. then oh well.
            // ToDo: maybe try..catch and just provide a blank guid?
            _entity = _system.AddNewEntity(_guids.NewGuid());
        }

        private void TestTearDown()
        {
            Assert.That(_entity != null);
            Assert.That(_entity.GetAllComponents().Count == 0,
                "Entity should have no components loaded");
            _system.RemoveEntity(_entity.Guid);
            _entity = null;

            Assert.That(_guids != null);
            Assert.That(_guids.ActiveGuids.Count == 0,
                "Guid Manager should have released all entities");
            _guids = null;

            Assert.That(_system != null);
            Assert.That(_system.GetAllEntities().Count == 0,
                "System Manager should have released all entities");
            _system = null;
        }

        [TestCase]
        public void TestIfSetUpProper()
        {
            TestSetUp();

            Assert.That(true == true);

            TestTearDown();
        }

        [TestCase]
        public void MultipleComponents_DifferentTypes()
        {
            TestSetUp();

            _system.AddComponentSystem<EntityFramework.ComponentInterfaces.ITagSystem>
                (new EntityFramework.Components.TagSystem());

            _system.AddNewComponentToEntity
                <EntityFramework.Components.Tag,
                 EntityFramework.ComponentInterfaces.ITagSystem>(_entity);

            TestTearDown();
        }

        //[TestCase]
        public void MultipleComponentSameType()
        {
            TestSetUp();

            //_system.AddComponentSystem<EntityFramework.

            TestTearDown();
        }
    }
}
