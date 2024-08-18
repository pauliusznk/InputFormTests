import { test, expect, Page } from '@playwright/test';
import { FormPage } from '../pages/FormPage';
import { ResponsePage } from '../pages/ResponsePage';
import { StringGenerator } from '../utilities/StringGenerator';
import { Submission } from '../models/Submission';

test.describe('FormTests', () => {
  let formPage: FormPage;
  let responsePage: ResponsePage;

  test.beforeEach(async ({ page }) => {
    formPage = new FormPage(page);
    responsePage = new ResponsePage(page);
  });

  test('CorrectSubmissionMinimumLengthValues', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(1),
      StringGenerator.generateRandomString(11),
      18,
      'Afghanistan',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await responsePage.checkResponse(submission);
  });

  test('CorrectSubmissionMaximumLengthValues', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(89),
      StringGenerator.generateRandomString(79),
      80,
      'Zimbabwe',
      StringGenerator.generateRandomString(2000)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await responsePage.checkResponse(submission);
  });

  test('IncorrectSubmissionEmptyFirstName', async () => {
    const submission = new Submission(
      '',
      StringGenerator.generateRandomString(79),
      80,
      'Zimbabwe',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.firstNameErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionFirstNameTooLong', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(90),
      StringGenerator.generateRandomString(79),
      80,
      'Zimbabwe',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.firstNameErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionEmptyLastName', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(20),
      '',
      80,
      'Zimbabwe',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.lastNameErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionTooShortLastName', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(20),
      StringGenerator.generateRandomString(10),
      80,
      'Zimbabwe',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.lastNameErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionTooLongLastName', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(20),
      StringGenerator.generateRandomString(80),
      80,
      'Zimbabwe',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.lastNameErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionEmptyAge', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(20),
      StringGenerator.generateRandomString(20),
      null,
      'Lithuania',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.ageErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionTooLittleAge', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(20),
      StringGenerator.generateRandomString(20),
      17,
      'Lithuania',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.ageErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });

  test('IncorrectSubmissionTooBigAge', async () => {
    const submission = new Submission(
      StringGenerator.generateRandomString(20),
      StringGenerator.generateRandomString(20),
      81,
      'Lithuania',
      StringGenerator.generateRandomString(1)
    );
    await formPage.goTo();
    await formPage.submitForm(submission);
    await formPage.ageErrorVisible();
    await responsePage.assertPageIsNotLoaded();
  });
});
