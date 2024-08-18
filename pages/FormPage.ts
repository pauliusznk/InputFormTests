import { Page, Locator, expect } from '@playwright/test';
import { Submission } from '../models/Submission';

export class FormPage {
  private page: Page;
  private firstNameInput: Locator;
  private lastNameInput: Locator;
  private ageInput: Locator;
  private countryInput: Locator;
  private notesInput: Locator;
  private submitButton: Locator;
  private firstNameCustomError: Locator;
  private lastNameCustomError: Locator;
  private ageCustomError: Locator;

  constructor(page: Page) {
    this.page = page;
    this.firstNameInput = page.locator('#firstname');
    this.lastNameInput = page.locator('#surname');
    this.ageInput = page.locator('#age');
    this.countryInput = page.locator('#country');
    this.notesInput = page.locator('#notes');
    this.submitButton = page.locator('input[type="submit"]');
    this.firstNameCustomError = page.locator('[name="firstnamevalidation"]');
    this.lastNameCustomError = page.locator('[name="surnamevalidation"]');
    this.ageCustomError = page.locator('[name="agevalidation"]');
  }

  async goTo(): Promise<void> {
    await this.page.goto('https://testpages.eviltester.com/styled/validation/input-validation.html');
  }

  async submitForm(submission: Submission): Promise<void> {
    await this.firstNameInput.fill(submission.firstName);
    await this.lastNameInput.fill(submission.secondName);
    await this.ageInput.fill(submission.age?.toString() || '');
    await this.countryInput.selectOption(submission.country);
    await this.notesInput.fill(submission.notes);
    await this.submitButton.click();
  }

  async firstNameErrorVisible(): Promise<void> {
    const isCustomErrorVisible = await this.firstNameCustomError.isVisible();
    const validationMessage = await this.firstNameInput.evaluate<string>(input => (input as HTMLInputElement).validationMessage);
    const isHtmlErrorVisible = !!validationMessage;
    expect(isCustomErrorVisible || isHtmlErrorVisible).toBeTruthy();
  }

  async lastNameErrorVisible(): Promise<void> {
    const isCustomErrorVisible = await this.lastNameCustomError.isVisible();
    const validationMessage = await this.lastNameInput.evaluate<string>(input => (input as HTMLInputElement).validationMessage);
    const isHtmlErrorVisible = !!validationMessage;
    expect(isCustomErrorVisible || isHtmlErrorVisible).toBeTruthy();
  }

  async ageErrorVisible(): Promise<void> {
    const isCustomErrorVisible = await this.ageCustomError.isVisible();
    const validationMessage = await this.ageInput.evaluate<string>(input => (input as HTMLInputElement).validationMessage);
    const isHtmlErrorVisible = !!validationMessage;
    expect(isCustomErrorVisible || isHtmlErrorVisible).toBeTruthy();
  }
}
