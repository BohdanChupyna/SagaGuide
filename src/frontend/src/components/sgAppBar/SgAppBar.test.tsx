import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, screen} from '@testing-library/react'

import {FrodoCharacterPage, renderWithProviders} from "../../Tests/testUtils";
import {charactersUrl, traitsUrl} from "../../redux/slices/api/apiSlice";
import {frodoCharacter} from "../../Tests/characterTestData";
import {SgAppBarAriaLabels} from "./SgAppBarAriaLabels";
import {SgCharacterSheetAriaLabels} from "../sgCharacterSheet/SgCharacterSheetAriaLabels";
import userEvent from "@testing-library/user-event";

export const handlers = [
    rest.get(`${charactersUrl}/${frodoCharacter.id}`, (req, res, ctx) => {
        return res(ctx.json(frodoCharacter ), ctx.delay(0))
    }),
]

const server = setupServer(...handlers)

// Enable API mocking before tests.
beforeAll(() => server.listen())

// Reset any runtime request handlers we may add during the tests.
afterEach(() => server.resetHandlers())

// Disable API mocking after the tests are done.
afterAll(() => server.close())

test('Can delete character', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>);
    })
    
    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();
    
    let deleteButton = screen.getByLabelText(SgAppBarAriaLabels.DeleteCharacterButton);
    expect(deleteButton).toBeInTheDocument();
    act(() =>{
        userEvent.click(deleteButton);
    })
    
    let dialog = await screen.findByLabelText(SgAppBarAriaLabels.DeleteCharacterDialog);
    expect(dialog).toBeInTheDocument();

    expect(await screen.findByText("Delete Frodo?")).toBeInTheDocument();
    
    let okButton = screen.getByLabelText(SgAppBarAriaLabels.DeleteCharacterDialogOkButton);
    expect(okButton).toBeInTheDocument();
    act(() =>{
        userEvent.click(okButton);
    })
    
    expect(screen.queryByText("Frodo")).not.toBeInTheDocument();
})