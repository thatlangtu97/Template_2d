using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour
{
//    public static TestTakeDamage instance;
//    public GameEntity entity;
//    public GameEntity entityTakeDamage;
//    private void Awake()
//    {
//        instance = this;
//    }
//    private void Update()
//    {
////        if (Input.GetKeyDown(KeyCode.T))
////        {
////            AddReactiveComponent();
////        }
//    }
//    public void AddReactiveComponent()
//    {
//        //GameEntity takeDamage;
//        //takeDamage = Contexts.sharedInstance.game.CreateEntity();
//        //takeDamage.AddTakeDamage( entity, entityTakeDamage, 10,null);
//        //takeDamage.Destroy();
//    }
//    public void AddEntity(GameEntity e)
//    {
//        if (entity == null)
//        {
//            entity = e;
//        }
//        else
//        {
//            entityTakeDamage = e;
//        }
//    }
    public Animator animator;
    private void Update()
    {
//        float x = Input.GetAxis("Horizontal");
//        animator.SetFloat("Blend",x,0.1f,Time.deltaTime);
        //RaycasExample();
        BoxcastExample();
    }

    public LayerMask maskRaycast;
    private void RaycasExample()
    {
        // Perform a single raycast using RaycastCommand and wait for it to complete
        // Setup the command and result buffers
        var results = new NativeArray<RaycastHit>(1, Allocator.TempJob);

        var commands = new NativeArray<RaycastCommand>(1, Allocator.TempJob);

        // Set the data of the first command
        Vector3 origin = Vector3.zero;

        Vector3 direction = Vector3.left;

        commands[0] = new RaycastCommand(origin, direction,100f,maskRaycast,1);

        // Schedule the batch of raycasts
        JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));

        // Wait for the batch processing job to complete
        handle.Complete();

        // Copy the result. If batchedHit.collider is null there was no hit
        RaycastHit batchedHit = results[0];
        if(batchedHit.collider)
            Debug.Log(batchedHit.collider.gameObject);
        // Dispose the buffers
        results.Dispose();
        commands.Dispose();
    }
    private void BoxcastExample()
    {
        // Perform a single boxcast using BoxcastCommand and wait for it to complete
        // Set up the command and result buffers
        var results = new NativeArray<RaycastHit>(1, Allocator.TempJob);
        var commands = new NativeArray<BoxcastCommand>(1, Allocator.TempJob);

        // Set the data of the first command
        Vector3 center = Vector3.zero;
        Vector2 halfExtents = Vector3.one * 0.5f;
        Quaternion orientation = Quaternion.identity;
        Vector3 direction = Vector3.left;

        commands[0] = new BoxcastCommand(center, halfExtents, orientation, direction);

        // Schedule the batch of boxcasts
        var handle = BoxcastCommand.ScheduleBatch(commands, results, 1,  default(JobHandle));

        // Wait for the batch processing job to complete
        handle.Complete();

        // Copy the result. If batchedHit.collider is null there was no hit
        RaycastHit batchedHit = results[0];
        if(batchedHit.collider)
            Debug.Log(batchedHit.collider.gameObject);
        // Dispose the buffers
        results.Dispose();
        commands.Dispose();
    }
}
