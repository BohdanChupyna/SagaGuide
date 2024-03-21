import React from 'react'
import {
    ApplyTraitModifierCost,
    ITraitModifier,
    TraitModifierCostAffectTypeEnum,
    TraitModifierCostTypeEnum
} from "./ITrait";


test('ApplyTraitModifierCost TraitModifierCostTypeEnum.Points calculate correctly', async () => {
    let modifier: ITraitModifier = {
      costType: TraitModifierCostTypeEnum.Points,
      pointsCost: 5,
      name: '',
      localNotes: null,
      tags: [],
      bookReferences: [],
      features: [],
      costAffectType: TraitModifierCostAffectTypeEnum.Total,
      canLevel: false,
      id: ''
    };
    
    let total = 0;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(5);
    
    modifier.pointsCost = -3;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(2);

    modifier.pointsCost = 0;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(2);
})

test('ApplyTraitModifierCost TraitModifierCostTypeEnum.Multiplier calculate correctly', async () => {
    let modifier: ITraitModifier = {
        costType: TraitModifierCostTypeEnum.Multiplier,
        pointsCost: 5,
        name: '',
        localNotes: null,
        tags: [],
        bookReferences: [],
        features: [],
        costAffectType: TraitModifierCostAffectTypeEnum.Total,
        canLevel: false,
        id: ''
    };

    let total = 2;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(10);

    modifier.pointsCost = -3;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(-30);

    modifier.pointsCost = 0;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(-0);
})

test('ApplyTraitModifierCost TraitModifierCostTypeEnum.Percentage calculate correctly', async () => {
    let modifier: ITraitModifier = {
        costType: TraitModifierCostTypeEnum.Percentage,
        pointsCost: 50,
        name: '',
        localNotes: null,
        tags: [],
        bookReferences: [],
        features: [],
        costAffectType: TraitModifierCostAffectTypeEnum.Total,
        canLevel: false,
        id: ''
    };

    let total = 8;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(12);

    modifier.pointsCost = -50;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(6);

    modifier.pointsCost = 0;
    total = ApplyTraitModifierCost(modifier, total);
    expect(total).toBe(6);
})