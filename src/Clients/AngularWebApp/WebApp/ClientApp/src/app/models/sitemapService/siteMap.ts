import { BaseEntity } from "../baseEntity";

export interface SiteMap extends BaseEntity{
    siteMapLink: string;
    pageId: string;
}