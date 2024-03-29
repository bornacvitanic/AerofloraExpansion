using System;
using System.Collections.Generic;
using AerofloraExpansion.Wind.Interfaces;
using UnityEngine;

namespace AerofloraExpansion.Wind
{
    public class WindTransmitter : MonoBehaviour
    {
        public static WindTransmitter Instance;
        
        [SerializeField] private WindController windController;
        private List<IWindAffected> windAffecteds = new();

        private void Awake()
        {
            Instance = this;
        }

        public void Add(IWindAffected windAffected)
        {
            if(!windAffecteds.Contains(windAffected))
                windAffecteds.Add(windAffected);
        }

        public void Remove(IWindAffected windAffected)
        {
            if (windAffecteds.Contains(windAffected))
                windAffecteds.Remove(windAffected);
        }

        private void Update()
        {
            foreach (var windAffected in windAffecteds)
            {
                windAffected.ApplyWindForce(windController.WindDirection, windController.WindStrength);
            }
        }
    }
}