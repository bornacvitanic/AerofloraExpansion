using System;
using System.Collections.Generic;
using AerofloraExpansion.Wind.Interfaces;
using UnityEngine;

namespace AerofloraExpansion.Wind
{
    public class WindTransmitter : MonoBehaviour
    {
        private static WindTransmitter instance;

        public static WindTransmitter Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<WindTransmitter>();
                return instance;
            }
        }

        [SerializeField] private WindController windController;
        private List<IWindAffected> windAffecteds = new();

        private void Awake()
        {
            instance = this;
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