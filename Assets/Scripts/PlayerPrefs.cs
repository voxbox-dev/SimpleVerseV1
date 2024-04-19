using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{
    public class PlayerPrefs : MonoBehaviour
    {

        int cookieCount = 0;

        private void OnEnable()
        {
            SpatialBridge.actorService.onActorJoined += HandleActorJoined;
        }

        private void OnDisable()
        {
            SpatialBridge.actorService.onActorJoined -= HandleActorJoined;
        }

        private void HandleActorJoined(ActorJoinedEventArgs args)
        {
            IActor actor = SpatialBridge.actorService.actors[args.actorNumber];

            SpatialBridge.coreGUIService.DisplayToastMessage(actor.displayName + " joined the space");

            // Subscribe to property changes
            actor.onCustomPropertiesChanged += (ActorCustomPropertiesChangedEventArgs customPropertiesArgs) =>
            {
                if (customPropertiesArgs.changedProperties.ContainsKey("cookies"))
                {
                    SpatialBridge.coreGUIService.DisplayToastMessage(actor.displayName + " has collected  " + customPropertiesArgs.changedProperties["cookies"] + " cookies");
                }
            };
        }

        private void CollectCookies(int cookies)
        {
            cookieCount += cookies;
            SpatialBridge.actorService.localActor.SetCustomProperty("cookies", cookieCount);
        }
    }
}