import { Continent } from './continent.model';
import { Language } from './language.model';

export class Country {
    code:string;
    name:string;
    phone:string;
    capital:string;
    currency:string;
    languages:Language[];
    emoji:string;
}
