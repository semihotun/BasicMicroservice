import { ClaimTypeValue } from "./claimTypeValue";

export class AccessToken {
    claims: ClaimTypeValue[];
    token: string;
    expiration: string;
}