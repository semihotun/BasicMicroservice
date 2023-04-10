import { BaseEntity } from "../baseEntity";

export interface PageSeo extends BaseEntity {
    pageTag: string;
    pageDescription: string;
}