using Fusion;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] private Renderer _renderer;

    /*
     * If a client changes a Networked Property on an object over which it has no StateAuthority the change is not synchronized
     * over the network but instead applied as a local prediction and can be overridden by changes from the StateAuthority in the future.
     * Be careful to only update Networked Properties on the StateAuthority if you want it to update on every client.
     */
    [Networked, OnChangedRender(nameof(ColorChanged))]
    public Color NetworkedColor { get; set; }


    private void Update()
    {
        if (HasStateAuthority && Input.GetMouseButtonDown(0))
        {
            // Changing the material color here directly does not work since this code is only executed on the client pressing the button and not on every client.
            NetworkedColor = new Color(Random.value, Random.value, Random.value, 1f);
        }

        /*
         * You could extend the Update() method to set the material color every frame and all clients will change the object's color eventually.
         * However, this is relatively costly and there is a more elegant solution: Change Detection.
         */
    }

    private void ColorChanged()
    {
        _renderer.material.color = NetworkedColor;
    }
}
