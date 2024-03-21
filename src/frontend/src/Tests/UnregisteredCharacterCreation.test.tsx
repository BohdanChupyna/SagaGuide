import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, fireEvent, screen, waitFor} from '@testing-library/react'
import {ICharacter} from "../domain/interfaces/character/ICharacter";
import {charactersUrl, unregisteredCharacterUrl} from "../redux/slices/api/apiSlice";
import {renderWithProviders} from "./testUtils";
import {getMockAuthStatus, mockAuthenticatedStatus} from "../setupTests";
import {SgAppBarAriaLabels} from "../components/sgAppBar/SgAppBarAriaLabels";
import {SgNavBarAriaLabels} from "../components/sgNavBar/SgNavBarAriaLabels";
import {SgCharacterSheetAriaLabels} from "../components/sgCharacterSheet/SgCharacterSheetAriaLabels";
import {AppStore} from "../redux/store";
import {CreateMemoryRouterNavigatedToSpecificPage} from "../routerSettings";
import {RouterProvider} from "react-router-dom";
import {Router as RemixRouter} from "@remix-run/router/dist/router";

const jsonData =
    {
        "userId": "00000000-0000-0000-0000-000000000000",
        "name": "Unregistered Character",
        "player": "",
        "campaign": "",
        "title": "",
        "handedness": "",
        "gender": "",
        "race": "",
        "religion": "",
        "age": 0.0,
        "height": 0.0,
        "weight": 0.0,
        "techLevel": 0,
        "size": 0.0,
        "hpLose": 0,
        "fpLose": 0,
        "totalPoints": 0,
        "attributes": [
            {
                "attribute": {
                    "type": "BasicMove",
                    "pointsCostPerLevel": 5,
                    "valueIncreasePerLevel": 1.0,
                    "dependOnAttributeType": "DexterityAndHealth",
                    "bookReference": {
                        "pageNumber": 17,
                        "sourceBook": "BasicSet"
                    },
                    "id": "04422688-c74c-4e0a-a4d1-be2f27f3c16d"
                },
                "spentPoints": 0,
                "id": "fe489212-10df-4c80-9587-c985fa59c68f"
            },
            {
                "attribute": {
                    "defaultValue": 10.0,
                    "type": "Strength",
                    "pointsCostPerLevel": 10,
                    "valueIncreasePerLevel": 1.0,
                    "bookReference": {
                        "pageNumber": 14,
                        "sourceBook": "BasicSet"
                    },
                    "id": "053a5747-aad1-402b-9a5b-e8a8b770fa8f"
                },
                "spentPoints": 0,
                "id": "0171d45a-7b1f-4bc7-9464-4834616c46a7"
            },
            {
                "attribute": {
                    "defaultValue": 10.0,
                    "type": "Health",
                    "pointsCostPerLevel": 10,
                    "valueIncreasePerLevel": 1.0,
                    "bookReference": {
                        "pageNumber": 15,
                        "sourceBook": "BasicSet"
                    },
                    "id": "0f6b9935-b6ee-4329-9c65-d5172461fe1f"
                },
                "spentPoints": 0,
                "id": "7c7c09f0-4cb6-42ad-b456-3be0ab01b1e7"
            },
            {
                "attribute": {
                    "type": "BasicSpeed",
                    "pointsCostPerLevel": 5,
                    "valueIncreasePerLevel": 0.25,
                    "dependOnAttributeType": "DexterityAndHealth",
                    "bookReference": {
                        "pageNumber": 17,
                        "sourceBook": "BasicSet"
                    },
                    "id": "22dc26b6-690a-4e02-8b7e-7c491bfdd1e3"
                },
                "spentPoints": 0,
                "id": "c86b8b4d-b0a5-440a-8991-22d748240417"
            },
            {
                "attribute": {
                    "type": "Perception",
                    "pointsCostPerLevel": 5,
                    "valueIncreasePerLevel": 1.0,
                    "dependOnAttributeType": "Intelligence",
                    "bookReference": {
                        "pageNumber": 16,
                        "sourceBook": "BasicSet"
                    },
                    "id": "2a208147-6ab9-4776-abac-a3f8b11ee724"
                },
                "spentPoints": 0,
                "id": "8e7e8845-a03a-4ba5-a7e3-f5a5d6718f57"
            },
            {
                "attribute": {
                    "type": "HitPoints",
                    "pointsCostPerLevel": 2,
                    "valueIncreasePerLevel": 1.0,
                    "dependOnAttributeType": "Strength",
                    "bookReference": {
                        "pageNumber": 16,
                        "sourceBook": "BasicSet"
                    },
                    "id": "2d3328b4-b808-4960-b42e-46c0840b36c6"
                },
                "spentPoints": 0,
                "id": "28f6212a-c480-4d44-a2c8-6d5835bd747a"
            },
            {
                "attribute": {
                    "defaultValue": 10.0,
                    "type": "Dexterity",
                    "pointsCostPerLevel": 20,
                    "valueIncreasePerLevel": 1.0,
                    "bookReference": {
                        "pageNumber": 15,
                        "sourceBook": "BasicSet"
                    },
                    "id": "5d38d3a9-e454-41b6-9387-e89ad557ca14"
                },
                "spentPoints": 0,
                "id": "4a3222f3-a242-4aa5-8180-0254dbd4c23e"
            },
            {
                "attribute": {
                    "type": "FatiguePoints",
                    "pointsCostPerLevel": 3,
                    "valueIncreasePerLevel": 1.0,
                    "dependOnAttributeType": "Health",
                    "bookReference": {
                        "pageNumber": 16,
                        "sourceBook": "BasicSet"
                    },
                    "id": "7bb8d32c-ccdd-4527-a967-1f90a9e72d5c"
                },
                "spentPoints": 0,
                "id": "1fca344e-00d1-4d8a-a6bc-d6352d707964"
            },
            {
                "attribute": {
                    "defaultValue": 10.0,
                    "type": "Intelligence",
                    "pointsCostPerLevel": 20,
                    "valueIncreasePerLevel": 1.0,
                    "bookReference": {
                        "pageNumber": 15,
                        "sourceBook": "BasicSet"
                    },
                    "id": "7e87f715-c559-484d-93da-75ff68b0dbee"
                },
                "spentPoints": 0,
                "id": "3e293431-527e-4431-a318-8d706f632cf2"
            },
            {
                "attribute": {
                    "type": "Will",
                    "pointsCostPerLevel": 5,
                    "valueIncreasePerLevel": 1.0,
                    "dependOnAttributeType": "Intelligence",
                    "bookReference": {
                        "pageNumber": 16,
                        "sourceBook": "BasicSet"
                    },
                    "id": "f32ee13d-89c2-45cd-9d05-16344d4490fd"
                },
                "spentPoints": 0,
                "id": "f46b24f9-7433-40e7-9216-f4e963a85c44"
            }
        ],
        "skills": [],
        "techniques": [],
        "traits": [],
        "equipments": [],
        "createdBy": "00000000-0000-0000-0000-000000000000",
        "createdOn": "2024-01-29T10:22:25.5987534Z",
        "modifiedBy": "00000000-0000-0000-0000-000000000000",
        "modifiedOn": "2024-01-29T10:22:25.5987536Z",
        "version": 0,
        "id": "9a72aab8-6f2d-42a3-9756-a2fe15f8e06a"
    }
    
const unregisteredCharacter: ICharacter = JSON.parse(JSON.stringify(jsonData));

const newCharacterName = "My new Character";
const registeredCharacter: ICharacter = JSON.parse(JSON.stringify(jsonData));
registeredCharacter.name = newCharacterName;
registeredCharacter.id = "b00446df-e1f2-40e8-b922-41badf1d2aa6";

export const handlers = [
    rest.get(unregisteredCharacterUrl, (req, res, ctx) => {
        return res(ctx.json(unregisteredCharacter), ctx.delay(0))
    }),

    rest.post(charactersUrl, (req, res, ctx) => {
        return res(ctx.json(registeredCharacter), ctx.delay(0))
    }),
    rest.get(`${charactersUrl}/${registeredCharacter.id}`, (req, res, ctx) => {
        return res(ctx.json(registeredCharacter), ctx.delay(0))
    })
]

const server = setupServer(...handlers)

// Enable API mocking before tests.
beforeAll(() => server.listen())

// Reset any runtime request handlers we may add during the tests.
afterEach(() => server.resetHandlers())

// Disable API mocking after the tests are done.
afterAll(() => server.close())

// #region Attribute tests 
test('Unregistered character can be created and saved after sign in', async () =>
{
    const router: RemixRouter = CreateMemoryRouterNavigatedToSpecificPage("/home");
    let store: AppStore;
    act(() =>{
        store = renderWithProviders(<RouterProvider router={router}/>).store;
    })
    
    let auth = getMockAuthStatus();
    expect(auth.isAuthenticated).toBeFalsy();

    let navBar = screen.getByLabelText(SgAppBarAriaLabels.OpenNavBarButton);
    act(() =>{
        fireEvent.click(navBar);
    })
    
    //Create new unregistered character
    let newCharacterButton = screen.getByLabelText(SgNavBarAriaLabels.NewCharacterPageButton);
    act(() =>{
        fireEvent.click(newCharacterButton);
    })
    
    let characterName = (await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)) as HTMLInputElement;
    expect(characterName).toBeInTheDocument();
    expect(characterName.value).toBe(unregisteredCharacter.name);
    expect(store!.getState().currentCharacter.character?.id).toBe("9a72aab8-6f2d-42a3-9756-a2fe15f8e06a");
    expect(router.state.location.pathname).toContain(`character/9a72aab8-6f2d-42a3-9756-a2fe15f8e06a`);
    
    act(() =>{
        fireEvent.change(characterName, { target: { value: newCharacterName } });
    })
    await waitFor(() => expect(characterName.value).toBe(newCharacterName))
    
    //Sign in and redirect to registered character 
    let SignInButton = screen.getByLabelText(SgAppBarAriaLabels.AccountSignInButton);
    act(() =>{
        fireEvent.click(SignInButton);
    })

    //await new Promise(resolve => setTimeout(resolve, 1000));
    await waitFor(() => expect(router.state.location.pathname).toContain(`character/${registeredCharacter.id}`));
    
    characterName = (await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)) as HTMLInputElement;
    expect(characterName.value).toBe(newCharacterName);
    let ds = store!.getState();
    expect(store!.getState().currentCharacter.character?.id).toBe(registeredCharacter.id);
})