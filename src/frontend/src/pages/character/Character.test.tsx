import React from 'react'
import { rest } from 'msw'
import { setupServer } from 'msw/node'
import {act, fireEvent, screen} from '@testing-library/react'
import Character from "./Character";
import {renderWithProviders} from "../../Tests/testUtils";
import {frodoCharacter, frodoCharacterId} from "../../Tests/characterTestData";
import {useAppDispatch} from "../../redux/hooks";
import {AttributeTypeEnum} from "../../domain/interfaces/attribute/IAttribute";
import {ICharacter} from "../../domain/interfaces/character/ICharacter";
import {changeCurrentCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {charactersUrl} from "../../redux/slices/api/apiSlice";
import {SgCharacterSheetAriaLabels} from "../../components/sgCharacterSheet/SgCharacterSheetAriaLabels";

export const handlers = [
    rest.get(`${charactersUrl}/${frodoCharacterId}`, (req, res, ctx) => {
        return res(ctx.json(frodoCharacter), ctx.delay(0))
    })
]

const server = setupServer(...handlers)

// Enable API mocking before tests.
beforeAll(() => server.listen())

// Reset any runtime request handlers we may add during the tests.
afterEach(() => server.resetHandlers())

// Disable API mocking after the tests are done.
afterAll(() => server.close())

const FrodoCharacterPage = () => {
    const dispatch = useAppDispatch();
    dispatch(changeCurrentCharacter(frodoCharacter as unknown as ICharacter));
    
    return (
        <Character />
    );
}

// #region Attribute tests 
test('Test attribute increase', async () => 
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let spentPoints = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} spent points`);
    expect(spentPoints.textContent).toBe("[0]");
    let iqInput = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} input`) as HTMLInputElement;
    expect(iqInput.value).toBe("10");
    
    act(() =>{
        fireEvent.change(iqInput, { target: { value: "11" } });
    })
    
    expect(iqInput.value).toBe("11");
    expect(spentPoints.textContent).toBe("[20]");
    
})

test('Test attribute decrease', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    
    let spentPoints = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} spent points`);
    expect(spentPoints.textContent).toBe("[0]");
    let iqInput = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} input`) as HTMLInputElement;
    expect(iqInput.value).toBe("10");

    act(() =>{
        fireEvent.change(iqInput, { target: { value: "9" } });
    })
    
    expect(iqInput.value).toBe("9");
    expect(spentPoints.textContent).toBe("[-20]");
})
// #endregion Attribute tests

// #region Skill tests
test('Test skill level increase when attribute is increased', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    
    let skillLevel = screen.getByLabelText("Archaeology level");
    expect(skillLevel.textContent).toBe("11");
    let skillInput = screen.getByLabelText("Archaeology input") as HTMLInputElement;
    expect(skillInput.value).toBe("8");

    let iqInput = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} input`) as HTMLInputElement;
    expect(iqInput.value).toBe("10");

    act(() =>{
        fireEvent.change(iqInput, { target: { value: "11" } });
    })
    
    expect(skillInput.value).toBe("8");
    expect(skillLevel.textContent).toBe("12");
})

test('Test skill level decrease when attribute is decreased', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let skillLevel = screen.getByLabelText("Archaeology level");
    expect(skillLevel.textContent).toBe("11");
    let skillInput = screen.getByLabelText("Archaeology input") as HTMLInputElement;
    expect(skillInput.value).toBe("8");

    let iqInput = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} input`) as HTMLInputElement;
    expect(iqInput.value).toBe("10");
    act(() =>{
        fireEvent.change(iqInput, { target: { value: "9" } });
    })

    expect(skillInput.value).toBe("8");
    expect(skillLevel.textContent).toBe("10");
})

test('Test skill level decrease according to rules', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let skillLevel = screen.getByLabelText("Archaeology level");
    expect(skillLevel.textContent).toBe("11");
    let skillInput = screen.getByLabelText("Archaeology input") as HTMLInputElement;
    expect(skillInput.value).toBe("8");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "7" } });
    })
    expect(skillInput.value).toBe("7");
    expect(skillLevel.textContent).toBe("10");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "6" } });
    })
    expect(skillLevel.textContent).toBe("10");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "5" } });
    })
    expect(skillLevel.textContent).toBe("10");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "4" } });
    })
    expect(skillLevel.textContent).toBe("10");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "3" } });
    })
    expect(skillLevel.textContent).toBe("9");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "2" } });
    })
    expect(skillLevel.textContent).toBe("9");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "1" } });
    })
    expect(skillLevel.textContent).toBe("8");
})

test('Test skill level increase according to rules', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let skillLevel = screen.getByLabelText("Archaeology level");
    expect(skillLevel.textContent).toBe("11");
    let skillInput = screen.getByLabelText("Archaeology input") as HTMLInputElement;
    expect(skillInput.value).toBe("8");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "1" } });
    })
    expect(skillInput.value).toBe("1");
    expect(skillLevel.textContent).toBe("8");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "2" } });
    })
    expect(skillLevel.textContent).toBe("9");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "3" } });
    })
    expect(skillLevel.textContent).toBe("9");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "4" } });
    })
    expect(skillLevel.textContent).toBe("10");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "5" } });
    })
    expect(skillLevel.textContent).toBe("10");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "6" } });
    })
    expect(skillLevel.textContent).toBe("10");
    act(() =>{
        fireEvent.change(skillInput, { target: { value: "7" } });
    })
    expect(skillLevel.textContent).toBe("10");

    act(() =>{
        fireEvent.change(skillInput, { target: { value: "8" } });
    })
    expect(skillLevel.textContent).toBe("11");
})
// #endregion Skill tests

// #region Characteristic tests
test('Test Characteristic increase', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let spentPoints = screen.getByLabelText(`${AttributeTypeEnum.Perception} spent points`);
    expect(spentPoints.textContent).toBe("[0]");
    let perceptionInput = screen.getByLabelText(`${AttributeTypeEnum.Perception} input`) as HTMLInputElement;
    expect(perceptionInput.value).toBe("10");

    act(() =>{
        fireEvent.change(perceptionInput, { target: { value: "11" } });
    })

    expect(perceptionInput.value).toBe("11");
    expect(spentPoints.textContent).toBe("[5]");

})

test('Test Characteristic decrease', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let spentPoints = screen.getByLabelText(`${AttributeTypeEnum.Perception} spent points`);
    expect(spentPoints.textContent).toBe("[0]");
    let perceptionInput = screen.getByLabelText(`${AttributeTypeEnum.Perception} input`) as HTMLInputElement;
    expect(perceptionInput.value).toBe("10");

    act(() =>{
        fireEvent.change(perceptionInput, { target: { value: "9" } });
    })
    
    expect(perceptionInput.value).toBe("9");
    expect(spentPoints.textContent).toBe("[-5]");

})

test('Test Characteristic increase when relative attribute is increased', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let spentPoints = screen.getByLabelText(`${AttributeTypeEnum.Perception} spent points`);
    expect(spentPoints.textContent).toBe("[0]");
    let perceptionInput = screen.getByLabelText(`${AttributeTypeEnum.Perception} input`) as HTMLInputElement;
    expect(perceptionInput.value).toBe("10");
    let iqInput = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} input`) as HTMLInputElement;
    expect(iqInput.value).toBe("10");

    act(() =>{
        fireEvent.change(iqInput, { target: { value: "11" } });
    })
    
    expect(perceptionInput.value).toBe("11");
    expect(spentPoints.textContent).toBe("[0]");

})

test('Test Characteristic decrease when relative attribute is decreased', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let spentPoints = screen.getByLabelText(`${AttributeTypeEnum.Perception} spent points`);
    expect(spentPoints.textContent).toBe("[0]");
    let perceptionInput = screen.getByLabelText(`${AttributeTypeEnum.Perception} input`) as HTMLInputElement;
    expect(perceptionInput.value).toBe("10");
    let iqInput = screen.getByLabelText(`${AttributeTypeEnum.Intelligence} input`) as HTMLInputElement;
    expect(iqInput.value).toBe("10");

    act(() =>{
        fireEvent.change(iqInput, { target: { value: "9" } });
    })
    
    expect(perceptionInput.value).toBe("9");
    expect(spentPoints.textContent).toBe("[0]");

})
// #endregion Characteristic tests

test('Test PointsStatistic section show correct data', async () =>
{
    act(() =>{
        renderWithProviders(<FrodoCharacterPage/>)
    })
    let totalPoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalPoints)  as HTMLInputElement;
    expect(totalPoints.value).toBe("100");

    let remainingPoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalRemainingPoints);
    expect(remainingPoints.textContent).toBe("Remaining Points:18");

    let attributePoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalAttributesPoints);
    expect(attributePoints.textContent).toBe("Attributes:23");

    let skillPoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalSkillsPoints);
    expect(skillPoints.textContent).toBe("Skills:16");

    let techniquesPoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalTechniquesPoints);
    expect(techniquesPoints.textContent).toBe("Techniques:3");

    let advantagesPoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalAdvantagesPoints);
    expect(advantagesPoints.textContent).toBe("Advantages:80");

    let disadvantagesPoints = screen.getByLabelText(SgCharacterSheetAriaLabels.CharacterTotalDisadvantagesPoints);
    expect(disadvantagesPoints.textContent).toBe("Disadvantages:-40");
})