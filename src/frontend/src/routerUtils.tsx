import {useLocation} from "react-router-dom";

export const IsOnCharacterPage = () => useLocation().pathname.includes("/character/");
export const IsOnCharactersPage = () => useLocation().pathname.includes("/characters/");