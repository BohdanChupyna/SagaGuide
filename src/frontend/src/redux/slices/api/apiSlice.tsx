import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import {ICharacter, ICharacterInfo} from "../../../domain/interfaces/character/ICharacter";
import {ISkill} from "../../../domain/interfaces/skill/ISkill";
import ITrait from "../../../domain/interfaces/trait/ITrait";
import {ITechnique} from "../../../domain/interfaces/technique/ITechnique";
import {IEquipment} from "../../../domain/interfaces/equipment/IEquipment";
import {User} from "oidc-client-ts";


export const baseUrl = process.env.REACT_APP_BACKEND_URI as string;
export const charactersUrl = `${baseUrl}/characters`;
export const unregisteredCharacterUrl = `${baseUrl}/characters/unregistered/`;
export const skillsUrl = `${baseUrl}/db/skills`;
export const techniquesUrl = `${baseUrl}/db/techniques`;
export const traitsUrl = `${baseUrl}/db/traits`;
export const equipmentsUrl = `${baseUrl}/db/equipments`;

const ruleBookCacheKeepTime: number = 30*60;

function getUser() {
    const partialKey = 'oidc.user:';
    for (let i = 0; i < localStorage.length; i++) {
        const key = localStorage.key(i);

        // Check if the key starts with the specified partialKey
        if (key && key.startsWith(partialKey)) {
            // Retrieve the value and add it to the matchingItems array
            const oidcStorage = localStorage.getItem(key);
            if(oidcStorage)
            {
                return User.fromStorageString(oidcStorage);
            }
        }
    }
    return null;
}

export const apiSlice = createApi({
    // The cache reducer expects to be added at `state.api` (already default - this is optional)
    reducerPath: 'api',
    baseQuery: fetchBaseQuery({
        baseUrl: `${baseUrl}/`,
        prepareHeaders: (headers, { getState }) => {
            // By default, if we have a token in the store, let's use that for authenticated requests
            const user = getUser();
            const token = user?.access_token;//(getState() as RootState).user.bearerJwt;
            headers.set('authorization', `Bearer ${token}`);
            
            return headers;
        },
    }),
    tagTypes: ['CurrentCharacter', 'Characters'],
    endpoints: builder => ({
        getCharacterById: builder.query<ICharacter, string>({
            query: characterId => `characters/${characterId}`,
            providesTags: ['CurrentCharacter'],
        }),
        getUnregisteredCharacter: builder.query<ICharacter, null>({
            query: characterId => `characters/unregistered/`,
        }),
        putCharacter: builder.mutation<ICharacter, ICharacter>({
            query: (character: ICharacter) => ({
                url: 'characters',
                method: 'PUT',
                body: character,
            }),
            invalidatesTags: ['CurrentCharacter'],
        }),
        postCharacter: builder.mutation<ICharacter, ICharacter|void>({
            query: (character: ICharacter|void) => ({
                url: 'characters',
                method: 'POST',
                body: character,
            }),
            invalidatesTags: ['Characters', 'CurrentCharacter'],
            
        }),
        deleteCharacter: builder.mutation<void, string>({
            query: characterId => ({
                url: `characters/${characterId}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Characters', 'CurrentCharacter'],
        }),
        getCharactersInfo: builder.query<ICharacterInfo[], string[]>({
            query: characterIds => `characters/info?${characterIds.map(id => `ids=${id}&`)}`,
            providesTags: ['Characters'],
        }),
        getSkills: builder.query<ISkill[], void>({
            query: () => "db/skills",
            keepUnusedDataFor: ruleBookCacheKeepTime,
        }),
        getTraits: builder.query<ITrait[], void>({
            query: () => "db/traits",
            keepUnusedDataFor: ruleBookCacheKeepTime,
        }),
        getTechniques: builder.query<ITechnique[], void>({
            query: () => "db/techniques",
            keepUnusedDataFor: ruleBookCacheKeepTime,
        }),
        getEquipment: builder.query<IEquipment[], void>({
            query: () => "db/equipments",
            keepUnusedDataFor: ruleBookCacheKeepTime,
        }),
    })
})


export const {useGetCharacterByIdQuery,
    useGetUnregisteredCharacterQuery,
    useGetCharactersInfoQuery,
    useGetSkillsQuery,
    useGetTraitsQuery,
    useGetTechniquesQuery,
    useGetEquipmentQuery,
    usePutCharacterMutation,
    usePostCharacterMutation,
    useDeleteCharacterMutation,} = apiSlice