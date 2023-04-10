import { BaseEntity } from "../baseEntity";

export interface Page extends BaseEntity {
    userId: string;
    content: string;
    header: string;
    pageDescription: string;
    pageSeoId: string;
}