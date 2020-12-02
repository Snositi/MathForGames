﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Scene
    {
        private Actor[] _actors;
        private Matrix3 _transform = new Matrix3();
        public bool Started { get; private set; }
        public Scene() { _actors = new Actor[0]; }
        public Matrix3 World { get { return _transform; } }


        //Collision Functions
        public void TestForCollision(Actor referenceEntity)
        {
            if (referenceEntity.Collidable == true)
                for (int i = 0; i < _actors.Length; i++)
                {
                    float displacement = (float)Math.Sqrt((float)Math.Pow((referenceEntity.WorldPosition.X - _actors[i].WorldPosition.X) * 32, 2)
                     + (float)Math.Pow((referenceEntity.WorldPosition.Y - _actors[i].WorldPosition.Y) * 32, 2));

                    if (displacement < (referenceEntity.CollisionRadius + _actors[i].CollisionRadius) && _actors[i].Collidable == true && referenceEntity != _actors[i])
                    { referenceEntity.isColliding = true; _actors[i].isColliding = true; }
                }
        }
        public bool TestForCollisionWith(Actor reference, Actor entity)
        {
            if (entity.Collidable == true && reference.Collidable == true)
            {
                float displacement = (float)Math.Sqrt((float)Math.Pow((reference.WorldPosition.X - entity.WorldPosition.X) * 32, 2)
                 + (float)Math.Pow((reference.WorldPosition.Y - entity.WorldPosition.Y) * 32, 2));

                if (displacement < (reference.CollisionRadius + entity.CollisionRadius) && entity.Collidable == true && reference != entity)
                { return true; }                
            }
            return false;
        }
        
        //Actor Position Functions
        public void AddActor(Actor actor)
        {
            //Create a new array with a size one greater than our old array
            Actor[] appendedArray = new Actor[_actors.Length + 1];
            //Copy the values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                appendedArray[i] = _actors[i];
            }
            //Set the last value in the new array to be the actor we want to add
            appendedArray[_actors.Length] = actor;
            //Set old array to hold the values of the new array
            _actors = appendedArray;
        }

        public bool RemoveActor(int index)
        {
            //Check to see if the index is outside the bounds of our array
            if (index < 0 || index >= _actors.Length)
            {
                return false;
            }

            bool actorRemoved = false;

            //Create a new array with a size one less than our old array 
            Actor[] newArray = new Actor[_actors.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                //If the current index is not the index that needs to be removed,
                //add the value into the old array and increment j
                if (i != index)
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (_actors[i].Started)
                        _actors[i].End();
                }
            }

            //Set the old array to be the tempArray
            _actors = newArray;
            return actorRemoved;
        }

        public bool RemoveActor(Actor actor)
        {
            //Check to see if the actor was null
            if (actor == null)
            {
                return false;
            }

            bool actorRemoved = false;
            //Create a new array with a size one less than our old array
            Actor[] newArray = new Actor[_actors.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                if (actor != _actors[i])
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (actor.Started)
                        actor.End();
                }
            }

            //Set the old array to the new array
            _actors = newArray;
            //Return whether or not the removal was successful
            return actorRemoved;
        }

        //Looping Functions
        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }

        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].End();
            }

            Started = false;
        }
    }
}
