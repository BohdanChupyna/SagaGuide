import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, fireEvent, screen} from '@testing-library/react'
// We're using our own custom render function and not RTL's render.

import {FrodoCharacterPage, renderWithProviders} from "../../Tests/testUtils";
import {charactersUrl, equipmentsUrl} from "../../redux/slices/api/apiSlice";
import {frodoCharacter} from "../../Tests/characterTestData";
import {SgAppBarAriaLabels} from "../sgAppBar/SgAppBarAriaLabels";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import {SgCharacterSheetAriaLabels} from "../sgCharacterSheet/SgCharacterSheetAriaLabels";
import {equipmentsTestData} from "../../Tests/equipmentsTestData";
import userEvent from "@testing-library/user-event";

export const handlers = [
    rest.get(`${equipmentsUrl}`, (req, res, ctx) => {
        return res(ctx.json(equipmentsTestData ), ctx.delay(0))
    }),
    rest.get(`${charactersUrl}/${frodoCharacter.id}`, (req, res, ctx) => {
        return res(ctx.json(frodoCharacter), ctx.delay(0))
    }),
]

const server = setupServer(...handlers)

// Enable API mocking before tests.
beforeAll(() => server.listen())

// Reset any runtime request handlers we may add during the tests.
afterEach(() => server.resetHandlers())

// Disable API mocking after the tests are done.
afterAll(() => server.close())


test('Can add equipment from RuleBookTab', async () =>
{
    act(() => {
        renderWithProviders(<FrodoCharacterPage/>);
    });
    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();

    let equipmentToAdd = equipmentsTestData.find(t => t.name === "Throwing Axe")!;
    expect(screen.queryByLabelText(SgCharacterSheetAriaLabels.getEquipmentRowLabelWithProvidedName(equipmentToAdd.name))).not.toBeInTheDocument();

    let openRuleBookTabButton = screen.getByLabelText(SgAppBarAriaLabels.OpenRuleBookButton);
    expect(openRuleBookTabButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(openRuleBookTabButton);
    });
    let equipmentsTabPanelButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.EquipmentsTabPanelButton);
    expect(equipmentsTabPanelButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(equipmentsTabPanelButton);
    });
    
    let searchInput = await screen.findByLabelText(SgRuleBookTabAriaLabels.SearchInput) as HTMLInputElement;
    expect(searchInput).toBeInTheDocument();
    act(() => {
        userEvent.type(searchInput, equipmentToAdd.name);
    });
    expect(searchInput).toHaveValue(equipmentToAdd.name);
    
    let equipmentPaperSection = await screen.findByLabelText(SgRuleBookTabAriaLabels.getEquipmentPaperLabel(equipmentToAdd));
    act(() => {
        fireEvent.dblClick(equipmentPaperSection);
    });

    let characterEquipmentRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getEquipmentRowLabelWithProvidedName(equipmentToAdd.name));
    expect(characterEquipmentRow).toBeInTheDocument();
})
