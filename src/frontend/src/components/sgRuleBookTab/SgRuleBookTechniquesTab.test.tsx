import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, fireEvent, screen} from '@testing-library/react'
// We're using our own custom render function and not RTL's render.

import {FrodoCharacterPage, renderWithProviders} from "../../Tests/testUtils";
import {charactersUrl, techniquesUrl} from "../../redux/slices/api/apiSlice";
import {frodoCharacter} from "../../Tests/characterTestData";
import {SgAppBarAriaLabels} from "../sgAppBar/SgAppBarAriaLabels";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import {SgCharacterSheetAriaLabels} from "../sgCharacterSheet/SgCharacterSheetAriaLabels";
import {techniquesTestData} from "../../Tests/techniquesTestData";
import userEvent from "@testing-library/user-event";

export const handlers = [
    rest.get(`${techniquesUrl}`, (req, res, ctx) => {
        return res(ctx.json(techniquesTestData ), ctx.delay(0))
    }),
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


test('Can add technique without specializations from RuleBookTab', async () =>
{
    act(() => {
        renderWithProviders(<FrodoCharacterPage/>);
    });
    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();

    let techniqueToAdd = techniquesTestData.find(t => t.name === "Off-Hand Weapon Training")!;
    expect(screen.queryByLabelText(SgCharacterSheetAriaLabels.getTechniqueRowLabelWithProvidedName(techniqueToAdd.name))).not.toBeInTheDocument();

    let openRuleBookTabButton = screen.getByLabelText(SgAppBarAriaLabels.OpenRuleBookButton);
    expect(openRuleBookTabButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(openRuleBookTabButton);
    });

    let techniquesTabPanelButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.TechniquesTabPanelButton);
    expect(techniquesTabPanelButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(techniquesTabPanelButton);
    });

    let techniquePaperSection = await screen.findByLabelText(SgRuleBookTabAriaLabels.getTechniquePaperLabel(techniqueToAdd));
    act(() => {
        fireEvent.dblClick(techniquePaperSection);
    });
    
    let characterTechniqueRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getTechniqueRowLabelWithProvidedName(techniqueToAdd.name));
    expect(characterTechniqueRow).toBeInTheDocument();
})

test('Can add technique with specializations from RuleBookTab', async () =>
{
    act(() => {
        renderWithProviders(<FrodoCharacterPage/>);
    });

    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();

    let techniqueToAdd = techniquesTestData.find(t => t.name === "Retain Weapon (@Ranged Weapon@)")!;
    expect(screen.queryByLabelText(SgCharacterSheetAriaLabels.getTechniqueRowLabelWithProvidedName(techniqueToAdd.name))).not.toBeInTheDocument();

    let openRuleBookTabButton = screen.getByLabelText(SgAppBarAriaLabels.OpenRuleBookButton);
    expect(openRuleBookTabButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(openRuleBookTabButton);
    });
    let techniquesTabPanelButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.TechniquesTabPanelButton);
    expect(techniquesTabPanelButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(techniquesTabPanelButton);
    });
 
    let techniquePaperSection = await screen.findByLabelText(SgRuleBookTabAriaLabels.getTechniquePaperLabel(techniqueToAdd));
    act(() => {
        fireEvent.dblClick(techniquePaperSection);
    });

    let optionalDialog = await screen.findByLabelText(SgRuleBookTabAriaLabels.TechniqueDialog);
    expect(optionalDialog).toBeInTheDocument();

    let specializationInput = await screen.findByLabelText(SgRuleBookTabAriaLabels.TechniqueDialogNameInput) as HTMLInputElement;
    expect(specializationInput).toBeInTheDocument();
    act(() => {
        userEvent.type(specializationInput, "Assault Rifle");
    });
    expect(specializationInput).toHaveValue("Assault Rifle");
    
    let okButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.TechniqueDialogOkButton);
    expect(okButton).toBeInTheDocument();
    act(() => {
        userEvent.click(okButton);
    });

    //let st2 = document.documentElement.outerHTML;
    let characterTechniqueRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getTechniqueRowLabelWithProvidedName("Retain Weapon (Assault Rifle)"));
    expect(characterTechniqueRow).toBeInTheDocument();
})