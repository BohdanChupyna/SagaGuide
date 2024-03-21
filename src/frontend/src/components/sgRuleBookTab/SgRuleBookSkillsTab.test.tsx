import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, fireEvent, screen} from '@testing-library/react'
// We're using our own custom render function and not RTL's render.

import {FrodoCharacterPage, renderWithProviders} from "../../Tests/testUtils";
import {charactersUrl, skillsUrl} from "../../redux/slices/api/apiSlice";
import {skillsTestData} from "../../Tests/skillsTestData";
import {frodoCharacter} from "../../Tests/characterTestData";
import {SgAppBarAriaLabels} from "../sgAppBar/SgAppBarAriaLabels";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import {SgCharacterSheetAriaLabels} from "../sgCharacterSheet/SgCharacterSheetAriaLabels";

export const handlers = [
    rest.get(`${skillsUrl}`, (req, res, ctx) => {
        return res(ctx.json(skillsTestData ), ctx.delay(0))
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


 test('Can add skill from RuleBookTab', async () => 
 {
     act(() => {
         renderWithProviders(<FrodoCharacterPage/>);
     });
     expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();
     
     let skillToAdd = skillsTestData[1];
     expect(screen.queryByLabelText(SgCharacterSheetAriaLabels.getSkillRowLabel(skillToAdd))).not.toBeInTheDocument();
     
     let openRuleBookTabButton = screen.getByLabelText(SgAppBarAriaLabels.OpenRuleBookButton);
     expect(openRuleBookTabButton).toBeInTheDocument();
     act(() => {
         fireEvent.click(openRuleBookTabButton);
     });
     
     let skillsTabPanelButton = await screen.findByLabelText(SgRuleBookTabAriaLabels.SkillsTabPanelButton);
     expect(skillsTabPanelButton).toBeInTheDocument();
     act(() => {
             fireEvent.click(skillsTabPanelButton);
     });

     let skillPaperSection = await screen.findByLabelText(SgRuleBookTabAriaLabels.getSkillPaperLabel(skillToAdd));
     act(() => {
        fireEvent.dblClick(skillPaperSection);
     });
     
     //console.log(document.documentElement.outerHTML);
     let characterSkillRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getSkillRowLabel(skillToAdd));
     expect(characterSkillRow).toBeInTheDocument();
 })