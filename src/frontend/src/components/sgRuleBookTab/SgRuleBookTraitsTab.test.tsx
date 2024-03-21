import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, fireEvent, screen} from '@testing-library/react'
// We're using our own custom render function and not RTL's render.

import {FrodoCharacterPage, renderWithProviders} from "../../Tests/testUtils";
import {charactersUrl, traitsUrl} from "../../redux/slices/api/apiSlice";
import {frodoCharacter} from "../../Tests/characterTestData";
import {SgAppBarAriaLabels} from "../sgAppBar/SgAppBarAriaLabels";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import {SgCharacterSheetAriaLabels} from "../sgCharacterSheet/SgCharacterSheetAriaLabels";
import userEvent from "@testing-library/user-event";
import {traitsTestData} from "../../Tests/traitTestData";

export const handlers = [
    rest.get(`${traitsUrl}`, (req, res, ctx) => {
        return res(ctx.json(traitsTestData ), ctx.delay(0))
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

test('Can add trait without modifiers from RuleBookTab', async () =>
{
    act(() => {
        renderWithProviders(<FrodoCharacterPage/>);
    });

    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();

    let traitToAdd = traitsTestData.find(t => t.name === "Absent-Mindedness")!;
    expect(screen.queryByLabelText(SgCharacterSheetAriaLabels.getTraitRowLabelWithProvidedName(traitToAdd.name))).not.toBeInTheDocument();

    let openRuleBookTabButton = screen.getByLabelText(SgAppBarAriaLabels.OpenRuleBookButton);
    expect(openRuleBookTabButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(openRuleBookTabButton);
    });

    let traitsTabPanelButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.TraitsTabPanelButton);
    expect(traitsTabPanelButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(traitsTabPanelButton);
    });

    let traitPaperSection = await screen.findByLabelText(SgRuleBookTabAriaLabels.getTraitPaperLabel(traitToAdd));
    act(() => {
        fireEvent.dblClick(traitPaperSection);
    });

    let characterTraitRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getTraitRowLabelWithProvidedName(traitToAdd.name));
    expect(characterTraitRow).toBeInTheDocument();
})

test('Can add trait with modifiers from RuleBookTab', async () =>
{
    act(() => {
        renderWithProviders(<FrodoCharacterPage/>);
    });

    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();

    let traitToAdd = traitsTestData.find(t => t.name === "Ally (@Who@)")!;
    expect(screen.queryByLabelText(SgCharacterSheetAriaLabels.getTraitRowLabelWithProvidedName(traitToAdd.name))).not.toBeInTheDocument();

    let openRuleBookTabButton = screen.getByLabelText(SgAppBarAriaLabels.OpenRuleBookButton);
    expect(openRuleBookTabButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(openRuleBookTabButton);
    });

    let traitsTabPanelButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.TraitsTabPanelButton);
    expect(traitsTabPanelButton).toBeInTheDocument();
    act(() => {
        fireEvent.click(traitsTabPanelButton);
    });

    let traitPaperSection = await screen.findByLabelText(SgRuleBookTabAriaLabels.getTraitPaperLabel(traitToAdd));
    act(() => {
        fireEvent.dblClick(traitPaperSection);
    });
    
    let characterTraitRow = await screen.queryByLabelText(SgCharacterSheetAriaLabels.getTraitRowLabelWithProvidedName(traitToAdd.name));
    expect(characterTraitRow).not.toBeInTheDocument();
    
    let optionalDialog = await screen.findByLabelText(SgRuleBookTabAriaLabels.TechniqueDialog);
    expect(optionalDialog).toBeInTheDocument();

    let specializationInput = screen.getByLabelText(SgRuleBookTabAriaLabels.TraitDialogSpecializationInput) as HTMLInputElement;
    expect(specializationInput).toBeInTheDocument();
    act(() => {
        userEvent.type(specializationInput, "Cat");
    });
    expect(specializationInput).toHaveValue("Cat");
    
    let minionModifier = screen.getByLabelText(SgRuleBookTabAriaLabels.getTraitModifierLabelWithProvidedName('Minion', 50));
    expect(minionModifier).toBeInTheDocument();
    act(() => {
        userEvent.click(minionModifier);
    })

    let pointsModifier = screen.getByLabelText(SgRuleBookTabAriaLabels.getTraitModifierLabelWithProvidedName('100% of your starting points', 5));
    expect(pointsModifier).toBeInTheDocument();
    act(() => {
        userEvent.click(pointsModifier);
    });
    
    let okButton = screen.getByLabelText(SgRuleBookTabAriaLabels.TraitDialogOkButton);
    expect(okButton).toBeInTheDocument();
    act(() => {
        userEvent.click(okButton);
    });
    
    // //let st2 = document.documentElement.outerHTML;
    characterTraitRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getTraitRowLabelWithProvidedName("Ally (Cat)"));
    expect(characterTraitRow).toBeInTheDocument();

    let characterTraitName = await screen.findByLabelText(SgCharacterSheetAriaLabels.getTraitNameLabelWithProvidedName("Ally (Cat)"));
    expect(characterTraitName).toBeInTheDocument();
    expect(characterTraitName.textContent).toBe("Ally (Cat)");

    let characterTraitCost = await screen.findByLabelText(SgCharacterSheetAriaLabels.getTraitCostLabelWithProvidedName("Ally (Cat)"));
    expect(characterTraitCost).toBeInTheDocument();
    expect(characterTraitCost.textContent).toBe("[55]");
})