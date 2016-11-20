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
            //_entity = _system.AddNewEntity(_guids.NewGuid());
        }

        private void TestTearDown()
        {
            //Assert.That(_entity != null);
            //Assert.That(_entity.GetAllComponents().Count == 0,
            //    "Entity should have no components loaded");
            //_system.RemoveEntity(_entity.Guid);
            //_entity = null;

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
        public void ComponentSystem_Poly_ATag_TagSys()
        {
            EntityFramework.Components.TagSystem tagSys =
                new Components.TagSystem();

            var ATag_TagSys =
                (EntityFramework.AComponentSystem
                    <EntityFramework.ComponentInterfaces.ITag>)tagSys;

            Assert.That(tagSys == ATag_TagSys);
        }

        [TestCase]
        public void ComponentSystem_Poly_ITag_TagSys()
        {
            EntityFramework.Components.TagSystem tagSys =
                new Components.TagSystem();

            var ITag_TagSys =
                (EntityFramework.IComponentSystem
                    <EntityFramework.ComponentInterfaces.ITag>)tagSys;

            Assert.That(tagSys == ITag_TagSys);
        }

        [TestCase]
        public void ComponentSystem_Poly_I_TagSys()
        {
            EntityFramework.Components.TagSystem tagSys =
                new Components.TagSystem();

            var I_TagSys =
                (EntityFramework.IComponentSystem)tagSys;

            Assert.That(tagSys == I_TagSys);
        }

        [TestCase]
        [ExpectedException(typeof(TypeAccessException))]
        public void ComponentSystem_Init_Tag_With_ITag()
        {
            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            tagSys.Init(typeof(EntityFramework.ComponentInterfaces.ITag));
            // This should not hit
            Assert.That(tagSys.GetGenerateType() == null);
        }

        [TestCase]
        public void ComponentSystem_Init_Tag_With_Tag()
        {
            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            tagSys.Init(typeof(EntityFramework.Components.Tag));
            Assert.That(tagSys.GetGenerateType() != null);
        }

        [TestCase]
        [ExpectedException(typeof(TypeLoadException))]
        public void ComponentSystem_Init_Tag_With_NonTag()
        {
            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            // The following should throw an error
            tagSys.Init(typeof(EntityFramework.Components.Children));
            // This should not hit
            Assert.That(tagSys.GetGenerateType() == null);
        }

        [TestCase]
        [ExpectedException(typeof(TypeAccessException))]
        public void ComponentSystem_Init_Tag_With_BaseComponent()
        {
            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            // The following should throw an error
            tagSys.Init(typeof(EntityFramework.Component));
            // This should not hit
            Assert.That(tagSys.GetGenerateType() == null);
        }

        [TestCase]
        public void MultipleComponents_DifferentTypes()
        {
            TestSetUp();

            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            tagSys.Init(typeof(EntityFramework.Components.Tag));

            EntityFramework.ComponentInterfaces.IChildrenSystem childSys =
                new EntityFramework.Components.ChildrenSystem();
            childSys.Init(typeof(EntityFramework.Components.Children));

            _system.AddComponentSystem<EntityFramework.ComponentInterfaces.ITagSystem>
                (tagSys);
            _system.AddComponentSystem<EntityFramework.ComponentInterfaces.IChildrenSystem>
                (childSys);

            Guid id = Guid.NewGuid();
            _system.AddNewEntity(id);

            _system.AddComponentToEntity
                <EntityFramework.ComponentInterfaces.ITagSystem>(id);
            _system.AddComponentToEntity
                <EntityFramework.ComponentInterfaces.IChildrenSystem>(id);

            _system.RemoveComponentFromEntity
                <EntityFramework.ComponentInterfaces.ITagSystem>(id);
            _system.RemoveComponentFromEntity
                <EntityFramework.ComponentInterfaces.IChildrenSystem>(id);

            _system.RemoveComponentSystem
                <EntityFramework.ComponentInterfaces.ITagSystem>();
            _system.RemoveComponentSystem
                <EntityFramework.ComponentInterfaces.IChildrenSystem>();

            _system.RemoveEntity(id);

            TestTearDown();
        }

        [TestCase]
        public void MultipleComponentSameType()
        {
            TestSetUp();

            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            tagSys.Init(typeof(EntityFramework.Components.Tag));

            _system.AddComponentSystem<EntityFramework.ComponentInterfaces.ITagSystem>
                (tagSys);

            Guid id = Guid.NewGuid();
            _system.AddNewEntity(id);

            _system.AddComponentToEntity
                <EntityFramework.ComponentInterfaces.ITagSystem>(id);

            _system.AddComponentToEntity
                <EntityFramework.ComponentInterfaces.ITagSystem>(id);

            _system.RemoveComponentSystem
                <EntityFramework.ComponentInterfaces.ITagSystem>();

            TestTearDown();
        }

        [TestCase]
        public void Entity_Add_Remove()
        {
            TestSetUp();

            EntityFramework.ComponentInterfaces.ITagSystem tagSys =
                new EntityFramework.Components.TagSystem();
            tagSys.Init(typeof(EntityFramework.Components.Tag));
            _system.AddComponentSystem<EntityFramework.ComponentInterfaces.ITagSystem>
                 (tagSys);

            Guid id = Guid.NewGuid();
            _system.AddNewEntity(id);

            _system.AddComponentToEntity
                <EntityFramework.ComponentInterfaces.ITagSystem>(id);

            _system.RemoveComponentFromEntity
                <EntityFramework.ComponentInterfaces.ITagSystem>(id);

            _system.RemoveComponentSystem
                <EntityFramework.ComponentInterfaces.ITagSystem>();

            _system.RemoveEntity(id);

            TestTearDown();
        }
    }
}
