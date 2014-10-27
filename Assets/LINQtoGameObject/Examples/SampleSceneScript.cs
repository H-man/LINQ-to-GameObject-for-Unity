﻿using UnityEngine;
using System.Collections;
using System.Linq;
using Unity.Linq; // using LINQ to GameObject

// This script attached to Root.
public class SampleSceneScript : MonoBehaviour
{
    void OnGUI()
    {
        var origin = GameObject.Find("Origin");

        if (GUILayout.Button("Parent"))
        {
            Debug.Log("------Parent");
            var parent = origin.Parent();
            Debug.Log(parent.name);
        }

        if (GUILayout.Button("Child"))
        {
            Debug.Log("------Child");
            var child = origin.Child("Sphere_B");
            Debug.Log(child.name);
        }

        if (GUILayout.Button("Descendants"))
        {
            Debug.Log("------Descendants");
            var descendants = origin.Descendants();
            foreach (var item in descendants)
            {
                Debug.Log(item.name);
            }
        }

        if (GUILayout.Button("name filter overload"))
        {
            Debug.Log("name filter overload");
            var filtered = origin.Descendants("Group");
            foreach (var item in filtered)
            {
                Debug.Log(item.name);
            }
        }

        if (GUILayout.Button("OfComponent"))
        {
            Debug.Log("------OfComponent");
            // get only SphereCollider
            var sphere = origin.Descendants().OfComponent<SphereCollider>();
            foreach (var item in sphere)
            {
                Debug.Log("Sphere:" + item.name + " Radius:" + item.radius);
            }
        }

        if (GUILayout.Button("LINQ"))
        {
            Debug.Log("------LINQ");
            // get children only ends with B
            var filter = origin.Children().Where(x => x.name.EndsWith("B"));
            foreach (var item in filter)
            {
                Debug.Log(item.name);
            }
        }

        if (GUILayout.Button("Add"))
        {
            origin.Add(new[] { new GameObject("lastChild1"), new GameObject("lastChild2"), new GameObject("lastChild3") });
            origin.AddFirst(new[] { new GameObject("firstChild1"), new GameObject("firstChild2"), new GameObject("firstChild3") });
            origin.AddBeforeSelf(new[] { new GameObject("beforeSelf1"), new GameObject("beforeSelf2"), new GameObject("beforeSelf3") });
            origin.AddAfterSelf(new[] { new GameObject("afterSelf1"), new GameObject("afterSelf2"), new GameObject("afterSelf3") });

            // Note, Cloned object around origin but original object is placed top of hierarchy.
            Resources.FindObjectsOfTypeAll<GameObject>()
                .Cast<GameObject>()
                .Where(x => x.Parent() == null && x.name != "Main Camera" && x.name != "Root" && x.name != "" && x.name != "HandlesGO")
                .Destroy();
        }

        if (GUILayout.Button("Destroy"))
        {
            // Destroy Cloned Object. Press button after Add Button.
            origin.transform.root.gameObject
                .Descendants()
                .Where(x => x.name.EndsWith("(Clone)"))
                .Destroy();
        }
    }
}