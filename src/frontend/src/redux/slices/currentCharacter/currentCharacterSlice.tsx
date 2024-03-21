import {AnyAction, createSlice, PayloadAction} from '@reduxjs/toolkit'
import {AppThunk, RootState} from "../../store";
import {ICharacter} from "../../../domain/interfaces/character/ICharacter";
import {AttributeTypeEnum} from "../../../domain/interfaces/attribute/IAttribute";
import {ISkill} from "../../../domain/interfaces/skill/ISkill";
import ITrait, {ITraitModifier} from "../../../domain/interfaces/trait/ITrait";
import {ITechnique} from "../../../domain/interfaces/technique/ITechnique";
import {
    _addCharacterEquipmentImpl,
    _addCharacterSkillImpl,
    _addCharacterTechniqueImpl,
    _addCharacterTraitImpl,
    _changeCharacterAttributeImpl,
    _changeCharacterSkillPointsImpl,
    _changeCharacterTechniquePointsImpl, _equipCharacterEquipmentByIdImpl, _removeCharacterEquipmentByIdImpl,
    _removeCharacterSkillByIdImpl, _removeCharacterTechniqueByIdImpl,
    _removeCharacterTraitByIdImpl
} from "../../../domain/interfaces/character/characterDomainLogicHelper";
import {IEquipment} from "../../../domain/interfaces/equipment/IEquipment";
import {ISgToast} from "../toasts/IToastProviderSlice";
import {grabToastWrapper} from "../toasts/toastsSlice";


// Define a type for the slice state
export interface SagaGuideCurrentCharacterState {
    isRuleBookOpen: boolean,
    character: ICharacter|null,
    toast: ISgToast|null,
}

// Define the initial state using that type
const initialState: SagaGuideCurrentCharacterState = {
    isRuleBookOpen: false,
    character: null,
    toast: null,
}

export const currentCharacterSlice = createSlice({
    name: 'SagaGuide',
    // `createSlice` will infer the state type from the `initialState` argument
    initialState,
    reducers: {
        changeCurrentCharacter: (state, action: PayloadAction<ICharacter|null>) => {
            state.character = action.payload;
        },
        changeCharacterName: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.name = action.payload;
        },
        changeCharacterPlayer: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.player = action.payload;
        },
        changeCharacterCampaign: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.campaign = action.payload;
        },
        changeCharacterTitle: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.title = action.payload;
        },
        changeCharacterHandedness: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.handedness = action.payload;
        },
        changeCharacterGender: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.gender = action.payload;
        },
        changeCharacterRace: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.race = action.payload;
        },
        changeCharacterReligion: (state, action: PayloadAction<string>) => {
            if(state.character === null) return;
            state.character.religion = action.payload;
        },
        changeCharacterHeight: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.height = action.payload;
        },
        changeCharacterWeight: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.weight = action.payload;
        },
        changeCharacterSize: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.size = action.payload;
        },
        changeCharacterTechLevel: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.techLevel = action.payload;
        },
        changeCharacterAge: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.age = action.payload;
        },
        changeCharacterHpLose: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.hpLose = action.payload;
        },
        changeCharacterFpLose: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.fpLose = action.payload;
        },
        changeCharacterTotalPoints: (state, action: PayloadAction<number>) => {
            if(state.character === null) return;
            state.character.totalPoints = action.payload;
        },
        changeCharacterAttribute: (state, action: PayloadAction<[AttributeTypeEnum, number]>) => {
            const [attributeType, newValue] = action.payload;
            _changeCharacterAttributeImpl(state, attributeType, newValue);
        },
        changeCharacterSkillPoints: (state, action: PayloadAction<[string, number]>) => {
            const [skillId, newPoints] = action.payload;
            _changeCharacterSkillPointsImpl(state, skillId, newPoints);
        },
        addCharacterSkill: (state, action: PayloadAction<[ISkill, string]>) => {
            const [skill, optionSpecialization] = action.payload;
            _addCharacterSkillImpl(state, skill, optionSpecialization);
            },
        removeCharacterSkillById: (state, action: PayloadAction<string>) => {
            _removeCharacterSkillByIdImpl(state, action.payload);
        },
        addCharacterTrait: (state, action: PayloadAction<[ITrait, string, ITraitModifier[]]>) => {
            const [trait, optionSpecialization, modifiers] = action.payload;
            _addCharacterTraitImpl(state, trait, optionSpecialization, modifiers);
        },
        removeCharacterTraitById: (state, action: PayloadAction<string>) => {
            _removeCharacterTraitByIdImpl(state, action.payload);
        },
        addCharacterTechnique: (state, action: PayloadAction<[ITechnique, string|null, string|null]>) => {
            const [technique, nameSpecialization, defaultNameSpecialization] = action.payload;
            _addCharacterTechniqueImpl(state, technique, nameSpecialization, defaultNameSpecialization);
        },
        changeCharacterTechniquePoints: (state, action: PayloadAction<[string, number]>) => {
            const [techniqueId, newPoints] = action.payload;
            _changeCharacterTechniquePointsImpl(state, techniqueId, newPoints);
        },
        removeCharacterTechniqueById: (state, action: PayloadAction<string>) => {
            _removeCharacterTechniqueByIdImpl(state, action.payload);
        },
        addCharacterEquipment: (state, action: PayloadAction<IEquipment>) => {
            _addCharacterEquipmentImpl(state, action.payload);
        },
        removeCharacterEquipmentById: (state, action: PayloadAction<string>) => {
            _removeCharacterEquipmentByIdImpl(state, action.payload);
        },
        equipCharacterEquipmentById: (state, action: PayloadAction<[string, boolean]>) => {
            const [equipmentId, isEquipped] = action.payload;
            _equipCharacterEquipmentByIdImpl(state, equipmentId, isEquipped);
        },
        openRuleBook: state => {
            state.isRuleBookOpen = true;
        },
        closeRuleBook: state => {
            state.isRuleBookOpen = false;
        },
        resetCharacterSliceToast: state => {
            state.toast = null;
        }
    }
})

export const {
    changeCurrentCharacter,
    changeCharacterName,
    changeCharacterPlayer,
    changeCharacterCampaign,
    changeCharacterTitle,
    changeCharacterHandedness,
    changeCharacterGender,
    changeCharacterRace,
    changeCharacterReligion,
    changeCharacterHeight,
    changeCharacterWeight,
    changeCharacterSize,
    changeCharacterTechLevel,
    changeCharacterAge,
    changeCharacterAttribute,
    changeCharacterHpLose,
    changeCharacterFpLose,
    changeCharacterTotalPoints,
    changeCharacterSkillPoints,
    addCharacterSkill,
    removeCharacterSkillById,
    addCharacterTrait,
    removeCharacterTraitById,
    addCharacterTechnique,
    changeCharacterTechniquePoints,
    removeCharacterTechniqueById,
    addCharacterEquipment,
    removeCharacterEquipmentById,
    equipCharacterEquipmentById,
    openRuleBook,
    closeRuleBook,
    resetCharacterSliceToast,
} = currentCharacterSlice.actions;

export const selectCharacter = (state: RootState) => state.currentCharacter.character;

export const selectIsRuleBookOpen = (state: RootState) => state.currentCharacter.isRuleBookOpen;
export const selectCharacterSliceToast = (state: RootState) => state.currentCharacter.toast;
export default currentCharacterSlice.reducer;
export function grabCharacterSliceToastWrapper(action: AnyAction): AppThunk {
    return grabToastWrapper(action, selectCharacterSliceToast, resetCharacterSliceToast());
}
