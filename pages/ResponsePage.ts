import { Page, Locator, expect } from '@playwright/test';
import { Submission } from '../models/Submission';

export class ResponsePage {
  private page: Page;
  private firstNameResponse: Locator;
  private secondNameResponse: Locator;
  private ageResponse: Locator;
  private countryResponse: Locator;
  private notesResponse: Locator;
  private responseHeader: Locator;

  constructor(page: Page) {
    this.page = page;
    this.firstNameResponse = page.locator('#firstname-value');
    this.secondNameResponse = page.locator('#surname-value');
    this.ageResponse = page.locator('#age-value');
    this.countryResponse = page.locator('#country-value');
    this.notesResponse = page.locator('#notes-value');
    this.responseHeader = page.getByText('Input Validation Response');
  }

  async assertPageIsNotLoaded(): Promise<void> {
    const isVisible = await this.responseHeader.isVisible();
    expect(isVisible).toBeFalsy();
  }

  async checkResponse(submission: Submission): Promise<void> {
    await this.checkField(this.firstNameResponse, submission.firstName, 'First name');
    await this.checkField(this.secondNameResponse, submission.secondName, 'Second name');
    await this.checkField(this.ageResponse, submission.age?.toString() || '', 'Age');
    await this.checkField(this.countryResponse, submission.country, 'Country');
    await this.checkField(this.notesResponse, submission.notes, 'Notes');
  }

  private async checkField(fieldLocator: Locator, expectedValue: string, fieldName: string): Promise<void> {
    const actualValue = await fieldLocator.innerText();
    if (actualValue !== expectedValue) {
      throw new Error(`${fieldName} mismatch: expected '${expectedValue}', got '${actualValue}'.`);
    }
  }
}
